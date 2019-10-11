﻿using Newtonsoft.Json;
using SO115App.Models.Classi.NUE;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Nue;
using System.Collections.Generic;
using System.Net.Http;

namespace SO115App.ExternalAPI.Fake.Nue
{
    public class GetSchedeContattoByCodiciFiscali : IGetSchedeContattoByCodiciFiscali
    {
        private HttpClient client = new HttpClient();

        public List<SchedaContatto> SchedeContattoFromCodiciFiscali(List<string> codiciFiscali)
        {
            List<SchedaContatto> listaSchede = new List<SchedaContatto>();
            var response = client.GetStringAsync(string.Format(Costanti.NueUrl + "/GetByCF/codiciFiscali={0}", codiciFiscali));
            listaSchede = JsonConvert.DeserializeObject<List<SchedaContatto>>(response.ToString());
            return listaSchede;
        }
    }
}