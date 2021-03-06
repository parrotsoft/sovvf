﻿using SO115App.Models.Classi.ServiziEsterni.NUE;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Nue;
using System;
using System.Collections.Generic;
using System.Text;

namespace SO115App.ExternalAPI.Fake.ImportOracle.SchedeContatto
{
    public class GetConteggioSchedeContatto : IGetConteggioSchede
    {
        private readonly IGetSchedeContatto _getSchedeContatto;
        private readonly string Competenza = "Competenza";
        private readonly string Conoscenza = "Conoscenza";
        private readonly string Differibile = "Differibile";

        public GetConteggioSchedeContatto(IGetSchedeContatto getSchedeContatto)
        {
            _getSchedeContatto = getSchedeContatto;
        }

        public InfoNue GetConteggio(string codiceSede)
        {
            var listaSchede = _getSchedeContatto.ListaSchedeContatto(codiceSede);
            var listaSchedeCompetenza = listaSchede.FindAll(x => x.Classificazione.Equals(Competenza) && x.Collegata == false);
            var listaSchedeConoscenza = listaSchede.FindAll(x => x.Classificazione.Equals(Conoscenza));
            var listaSchedeDifferibile = listaSchede.FindAll(x => x.Classificazione.Equals(Differibile));
            return new InfoNue
            {
                TotaleSchede = new ContatoreNue
                {
                    ContatoreTutte = listaSchede.Count,
                    ContatoreDaGestire = listaSchedeDifferibile.FindAll(x => !x.Gestita).Count,
                },
                CompetenzaSchede = new ContatoreNue
                {
                    ContatoreTutte = listaSchedeCompetenza.Count,
                },
                ConoscenzaSchede = new ContatoreNue
                {
                    ContatoreTutte = listaSchedeConoscenza.Count,
                },
                DifferibileSchede = new ContatoreNue
                {
                    ContatoreTutte = listaSchedeDifferibile.Count,
                    ContatoreDaGestire = listaSchedeDifferibile.FindAll(x => !x.Gestita).Count,
                }
            };
        }
    }
}
