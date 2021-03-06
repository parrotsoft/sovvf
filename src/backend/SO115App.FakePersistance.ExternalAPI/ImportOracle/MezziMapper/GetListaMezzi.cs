﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SO115App.API.Models.Classi.Condivise;
using SO115App.ExternalAPI.Fake.Classi.DTOOracle;
using SO115App.Models.Classi.Condivise;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Mezzi;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SO115App.ExternalAPI.Fake.ImportOracle.DistaccamentiMapper;

namespace SO115App.ExternalAPI.Fake.ImportOracle.MezziMapper
{
    public class GetListaMezzi : IGetListaMezzi

    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public GetListaMezzi(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<List<Mezzo>> Get(string CodSede)
        {
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("test");
            var response = await _client.GetAsync($"{_configuration.GetSection("OracleImplementation").GetSection(CodSede).GetSection("UrlAPIMezzi").Value}/GetListaMezziUtilizzabili?CodSede={CodSede}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using HttpContent content = response.Content;
            string data = await content.ReadAsStringAsync().ConfigureAwait(false);
            var ListaMezziOracle = JsonConvert.DeserializeObject<List<ORAAutomezzi>>(data);

            return MapListaMezziOraInMongoDB(ListaMezziOracle);
        }

        private List<Mezzo> MapListaMezziOraInMongoDB(List<ORAAutomezzi> ListaMezziOracle)
        {
            List<Mezzo> ListaMezzi = new List<Mezzo>();

            foreach (ORAAutomezzi OraM in ListaMezziOracle)
            {
                GetDistaccamentiByCodSede GetDistaccamentiByCodSede = new GetDistaccamentiByCodSede(_client, _configuration);
                List<Distaccamento> distaccamenti = GetDistaccamentiByCodSede.GetListaDistaccamenti(OraM.COD_COMANDO);
                var d = distaccamenti.Find(x => x.CodDistaccamento.Equals(OraM.COD_DISTACCAMENTO));

                var sede = new Sede(OraM.COD_COMANDO + "." + OraM.COD_DISTACCAMENTO, d.DescDistaccamento, d.Indirizzo, new Coordinate(1, 1), "", "", "", "", "");

                Mezzo mezzo = new Mezzo(OraM.COD_AUTOMEZZO.ToString(),
                    OraM.COD_MODELLO_MEZZO,
                    OraM.STATO,
                    OraM.DISTACCAMENTO,
                    OraM.COD_DESTINAZIONE,
                    sede,
                    new Coordinate(1, 1))

                {
                    Genere = OraM.COD_GENERE_MEZZO,
                };
                ListaMezzi.Add(mezzo);
            }

            return ListaMezzi;
        }
    }
}
