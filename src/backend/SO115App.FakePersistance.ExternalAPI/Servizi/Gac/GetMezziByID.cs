﻿using Newtonsoft.Json;
using SO115App.API.Models.Classi.Condivise;
using SO115App.ExternalAPI.Fake.Classi.Gac;
using SO115App.ExternalAPI.Fake.Classi.Utility;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Gac;
using System.Collections.Generic;
using System.Net.Http;

namespace SO115App.ExternalAPI.Fake.Servizi.Gac
{
    public class GetMezziByID : IGetMezziById
    {
        private readonly HttpClient _client;
        private readonly MapMezzoDTOsuMezzo _mapper;

        public GetMezziByID(HttpClient client)
        {
            _client = client;
        }

        public List<Mezzo> Get(List<string> codiceMezzo)
        {
            var response = _client.GetAsync($"{Costanti.GacGetID}?codiciMezzo={codiceMezzo}").ToString();
            var listaMezzoDTO = JsonConvert.DeserializeObject<List<MezzoDTO>>(response);
            return _mapper.MappaMezzoDTOsuMezzo(listaMezzoDTO);
        }
    }
}
