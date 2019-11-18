﻿//-----------------------------------------------------------------------
// <copyright file="SetGestita.cs" company="CNVVF">
// Copyright (C) 2017 - CNVVF
//
// This file is part of SOVVF.
// SOVVF is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
//
// SOVVF is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.
// </copyright>
//-----------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using SO115App.ExternalAPI.Fake.Classi;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Nue;
using System.Collections.Generic;
using System.Net.Http;

namespace SO115App.ExternalAPI.Fake.Nue
{
    /// <summary>
    ///   Classe che aggiorna la stato della scheda contatto in gestita e di conseguenza in letta
    /// </summary>

    public class SetGestita : ISetStatoGestioneSchedaContatto
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public SetGestita(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        /// <summary>
        ///   Metodo che invia una richiesta POST sul servizio Nue per aggiornare la stato della
        ///   scheda contatto in gestita e di conseguenza in letta
        /// </summary>
        /// <param name="codiceScheda">il codice della scheda contatto</param>
        /// <param name="codiceSede">il codice sede dell'operatore</param>
        /// <param name="codiceFiscale">il codice fiscale dell'operatore</param>
        /// <param name="gestita">la booleana gestita</param>
        public void Gestita(string codiceScheda, string codiceSede, string codiceFiscale, bool gestita)
        {
            var stringContent = new FormUrlEncodedContent(new[]
                                {
                                    new KeyValuePair<string, string>("codiceScheda", codiceScheda),
                                    new KeyValuePair<string, string>("codiceSede", codiceSede),
                                    new KeyValuePair<string, string>("codiceFiscale", codiceFiscale),
                                    new KeyValuePair<string, string>("gestita", gestita.ToString()),
                                });

            _client.PostAsync(_configuration.GetSection("UrlExternalApi").GetSection("NueApi").Value + Costanti.NueSetGestita, stringContent);
        }
    }
}
