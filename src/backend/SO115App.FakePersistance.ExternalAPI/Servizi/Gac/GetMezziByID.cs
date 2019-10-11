﻿using Newtonsoft.Json;
using SO115App.API.Models.Classi.Condivise;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Gac;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SO115App.ExternalAPI.Fake.Servizi.Gac
{
    public class GetMezziByID : IGetMezziById
    {
        private readonly HttpClient _client;

        public GetMezziByID(HttpClient client)
        {
            _client = client;
        }

        public List<Mezzo> Get(List<string> codiceMezzo)
        {
            var response = _client.GetAsync($"{Costanti.GacGetID}?codiciMezzo={codiceMezzo}").ToString();
            return JsonConvert.DeserializeObject<List<Mezzo>>(response);
        }
    }
}