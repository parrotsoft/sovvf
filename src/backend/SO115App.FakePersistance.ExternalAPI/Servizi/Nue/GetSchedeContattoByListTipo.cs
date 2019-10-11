﻿using Newtonsoft.Json;
using SO115App.Models.Classi.NUE;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Nue;
using System.Collections.Generic;
using System.Net.Http;

namespace SO115App.ExternalAPI.Fake.Nue
{
    public class GetSchedeContattoByListTipo : IGetSchedeContattoByTipo
    {
        private HttpClient client = new HttpClient();

        public List<SchedaContatto> SchedeContattoFromListTipo(List<string> classificazione)
        {
            List<SchedaContatto> listaSchede = new List<SchedaContatto>();
            var response = client.GetStringAsync(string.Format(Costanti.NueUrl + "/GetByTipo/classificazione={0}", classificazione));
            listaSchede = JsonConvert.DeserializeObject<List<SchedaContatto>>(response.ToString());
            return listaSchede;
        }
    }
}