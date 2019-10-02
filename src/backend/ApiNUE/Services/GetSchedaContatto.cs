﻿using Newtonsoft.Json;
using SO115App.ApiNUE.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SO115App.ApiNUE.Services
{
    public class GetSchedaContatto
    {
        private const string SchedeContattoJson = "Fake/SchedeContatto.json";

        public List<SchedaContatto> GetList()
        {
            const string filepath = SchedeContattoJson;
            string json;

            using (var r = new StreamReader(filepath))
            {
                json = r.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<SchedaContatto>>(json);
        }

        public SchedaContatto GetSchedaContattoAttuale(string codiceSede, string codiceOperatore)
        {
            var listaSchedeContatto = GetList();

            if (codiceOperatore == null) return listaSchedeContatto.Find(x =>
                 x.OperatoreChiamata.CodiceSede.Equals(codiceSede));

            return listaSchedeContatto.Find(x =>
                x.OperatoreChiamata.CodiceSede.Equals(codiceSede)
                && x.OperatoreChiamata.CodicePostazioneOperatore.Equals(codiceOperatore));
        }

        public List<SchedaContatto> GetSchedeContattoFromCodiciFiscali(List<string> codiciFiscali)
        {
            var listaSchedeContatto = GetList();

            return codiciFiscali.Select(codice => listaSchedeContatto.Find(x => x.OperatoreChiamata.CodiceFiscale.Equals(codice))).ToList();
        }

        public List<SchedaContatto> GetSchedeContattoFromCodiceSede(string codice)
        {
            return GetList().FindAll(x => x.OperatoreChiamata.CodiceSede.Equals(codice));
        }

        public List<SchedaContatto> GetSchedeContattoTimeSpan(DateTime dataDa, DateTime dataA)
        {
            return GetList().FindAll(x => x.DataInserimento >= dataDa && x.DataInserimento <= dataA);
        }

        public List<SchedaContatto> GetSchedeContattoLetta(bool letta)
        {
            return GetList().FindAll(x => x.Letta.Equals(letta));
        }

        public List<SchedaContatto> GetSchedeContattoGestita(bool gestita)
        {
            return GetList().FindAll(x => x.Gestita.Equals(gestita));
        }

        public List<SchedaContatto> GetSchedeContattoFromListTipo(List<string> classificazione)
        {
            var listaSchedeContatto = GetList();

            return classificazione.Select(classe => listaSchedeContatto.Find(x => x.Classificazione.Equals(classe))).ToList();
        }

        public List<SchedaContatto> GetSchedeContattoFromText(string testolibero)
        {
            var listaSchede = GetList();

            return (from schedaContatto in listaSchede let schedacontattoJson = JsonConvert.SerializeObject(schedaContatto) where schedacontattoJson.Contains(testolibero) select schedaContatto).ToList();
        }

        public List<SchedaContatto> GetSchedeContattoBySpatialArea(Coordinate topRight, Coordinate bottomLeft)
        {
            var listaSchede = GetList();
            var listaSchedeFiltered = new List<SchedaContatto>();

            listaSchedeFiltered.AddRange(listaSchede.Where(x => (x.Localita.Coordinate.Latitudine >= bottomLeft.Latitudine) && (x.Localita.Coordinate.Latitudine <= topRight.Latitudine) && ((x.Localita.Coordinate.Longitudine >= bottomLeft.Longitudine) && (x.Localita.Coordinate.Longitudine <= topRight.Longitudine))));

            return listaSchedeFiltered;
        }
    }
}