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
using SO115App.API.Models.Classi.Condivise;
using SO115App.API.Models.Classi.Soccorso;
using SO115App.FakePersistence.JSon.Utility;
using SO115App.FakePersistenceJSon.Classi;
using SO115App.FakePersistenceJSon.Utility;
using SO115App.Models.Servizi.CQRS.Commands.GestioneSoccorso.GestionePartenza.AggiornaStatoMezzo;
using SO115App.Models.Servizi.Infrastruttura.Composizione;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SO115App.FakePersistenceJSon.Composizione
{
    /// <summary>
    ///   La classe aggiorna i dati dell'intervento (squadre, mezzo e richiesta) in seguito al
    ///   cambio stato di un mezzo sull'intervento
    /// </summary>
    public class UpdateStatoPartenza : IUpdateStatoPartenze
    {
        /// <summary>
        ///   Il metodo accetta in firma il command, e in seguito al cambio di stato di uno o più
        ///   mezzi aggiorna le informazioni relative alla richiesta a cui quel mezzo è associato
        /// </summary>
        /// <param name="command">il command in ingresso</param>
        public void Update(AggiornaStatoMezzoCommand command)
        {
            var filepath = CostantiJson.ListaRichiesteAssistenza;
            var filePathMezzi = CostantiJson.Mezzo;
            var filePathSquadre = CostantiJson.SquadreComposizione;
            string json;
            string jsonMezzi;
            string jsonSquadre;
            using (var r = new StreamReader(filepath))
            {
                json = r.ReadToEnd();
            }

            using (var r = new StreamReader(filePathMezzi))
            {
                jsonMezzi = r.ReadToEnd();
            }

            using (var r = new StreamReader(filePathSquadre))
            {
                jsonSquadre = r.ReadToEnd();
            }

            var conferma = new ConfermaPartenze();
            var richiestaNew = new RichiestaAssistenzaDTO();
            var listaRichieste = JsonConvert.DeserializeObject<List<RichiestaAssistenzaDTO>>(json);
            var listaMezzi = JsonConvert.DeserializeObject<List<Mezzo>>(jsonMezzi);
            var listaSquadre = JsonConvert.DeserializeObject<List<ComposizioneSquadre>>(jsonSquadre);

            if (listaRichieste != null)
            {
                var listaRichiesteNew = new List<RichiestaAssistenza>();
                var richiestaDTO = listaRichieste.FirstOrDefault(x => x.Cod == command.Richiesta.Codice);
                listaRichieste.Remove(richiestaDTO);

                foreach (var richiesta in listaRichieste)
                {
                    listaRichiesteNew.Add(MapperDTO.MapRichiestaDTOtoRichiesta(richiesta));
                }

                listaRichiesteNew.Add(command.Richiesta);

                var jsonListaPresente = JsonConvert.SerializeObject(listaRichiesteNew);
                System.IO.File.WriteAllText(CostantiJson.ListaRichiesteAssistenza, jsonListaPresente);
            }
            else
            {
                var listaRichiesteNew = new List<RichiestaAssistenza> { command.Richiesta };

                string jsonNew = JsonConvert.SerializeObject(listaRichiesteNew);
                System.IO.File.WriteAllText(CostantiJson.ListaRichiesteAssistenza, jsonNew);
            }

            foreach (var composizione in command.Richiesta.Partenze)
            {
                foreach (var mezzo in listaMezzi.Where(mezzo => mezzo.Codice == composizione.Partenza.Mezzo.Codice))
                {
                    mezzo.Stato = composizione.Partenza.Mezzo.Stato;
                }

                foreach (var composizioneSquadra in listaSquadre)
                {
                    foreach (var squadra in composizione.Partenza.Squadre.Where(squadra => composizioneSquadra.Squadra.Id == squadra.Id))
                    {
                        composizioneSquadra.Squadra.Stato = squadra.Stato;
                    }
                }
            }

            var jsonListaMezzi = JsonConvert.SerializeObject(listaMezzi);
            System.IO.File.WriteAllText(CostantiJson.Mezzo, jsonListaMezzi);

            var jsonListaSquadre = JsonConvert.SerializeObject(listaSquadre);
            System.IO.File.WriteAllText(CostantiJson.SquadreComposizione, jsonListaSquadre);
        }
    }
}
