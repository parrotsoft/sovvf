﻿//-----------------------------------------------------------------------
// <copyright file="UpDateRichiesta.cs" company="CNVVF">
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
using Newtonsoft.Json;
using SO115App.API.Models.Classi.Composizione;
using SO115App.API.Models.Servizi.Infrastruttura.GestioneSoccorso;
using SO115App.FakePersistence.JSon.Utility;
using SO115App.Models.Servizi.CQRS.Commands.GestioneSoccorso.GestionePartenza.AggiornaStatoMezzo;
using SO115App.Models.Servizi.Infrastruttura.Composizione;
using SO115App.Models.Servizi.Infrastruttura.GestioneSoccorso.Mezzi;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Gac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SO115App.ExternalAPI.Fake.Composizione
{
    /// <summary>
    ///   La classe aggiorna i dati dell'intervento (squadre, mezzo e richiesta) in seguito al
    ///   cambio stato di un mezzo sull'intervento
    /// </summary>
    public class UpdateStatoPartenzaExt : IUpdateStatoPartenze
    {
        private readonly ISetStatoOperativoMezzo _setStatoOperativoMezzo;
        private readonly ISetMovimentazione _setMovimentazione;
        private readonly IUpDateRichiestaAssistenza _upDateRichiestaAssistenza;

        /// <summary>
        ///   Costruttore della classe
        /// </summary>
        public UpdateStatoPartenzaExt(ISetMovimentazione setMovimentazione, ISetStatoOperativoMezzo setStatoOperativoMezzo, IUpDateRichiestaAssistenza upDateRichiestaAssistenza)
        {
            _setMovimentazione = setMovimentazione;
            _setStatoOperativoMezzo = setStatoOperativoMezzo;
            _upDateRichiestaAssistenza = upDateRichiestaAssistenza;
        }

        /// <summary>
        ///   Il metodo accetta in firma il command, e in seguito al cambio di stato di uno o più
        ///   mezzi aggiorna le informazioni relative alla richiesta a cui quel mezzo è associato
        /// </summary>
        /// <param name="command">il command in ingresso</param>

        public void Update(AggiornaStatoMezzoCommand command)
        {
            var filePathSquadre = CostantiJson.SquadreComposizione;
            string jsonSquadre;

            using (var r = new StreamReader(filePathSquadre))
            {
                jsonSquadre = r.ReadToEnd();
            }

            var conferma = new ConfermaPartenze();
            var listaCodiceSede = new List<string>
            {
                command.CodiceSede
            };
            var listaSquadre = JsonConvert.DeserializeObject<List<ComposizioneSquadre>>(jsonSquadre);

            _upDateRichiestaAssistenza.UpDate(command.Richiesta);

            var dataMovimentazione = DateTime.UtcNow;
            _setMovimentazione.Set(command.IdMezzo, command.Richiesta.Codice, command.StatoMezzo, dataMovimentazione); //TODO IMPLEMENTARE CON GAC (PER SPER SCRIVERE DIRETTAMENTE SU DB)
            //_setStatoOperativoMezzo.Set(command.CodiceSede, command.IdMezzo, command.StatoMezzo, command.Richiesta.Codice);//TODO CANCELLARE NON PIU' NECESSARIO

            foreach (var partenza in command.Richiesta.Partenze)
            {
                foreach (var composizioneSquadra in listaSquadre)
                {
                    foreach (var squadra in partenza.Partenza.Squadre.Where(squadra => composizioneSquadra.Squadra.Id == squadra.Id))
                    {
                        composizioneSquadra.Squadra.Stato = squadra.Stato;
                    }
                }
            }

            var jsonListaSquadre = JsonConvert.SerializeObject(listaSquadre);
            File.WriteAllText(CostantiJson.SquadreComposizione, jsonListaSquadre);
        }
    }
}
