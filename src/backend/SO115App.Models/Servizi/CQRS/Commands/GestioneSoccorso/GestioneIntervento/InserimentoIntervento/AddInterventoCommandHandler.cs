﻿//-----------------------------------------------------------------------
// <copyright file="AddInterventoCommandHandler.cs" company="CNVVF">
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
using CQRS.Commands;
using SO115App.API.Models.Classi.Soccorso;
using SO115App.API.Models.Classi.Soccorso.Eventi;
using SO115App.API.Models.Classi.Soccorso.Eventi.Segnalazioni;
using SO115App.API.Models.Servizi.Infrastruttura.GestioneSoccorso;

namespace DomainModel.CQRS.Commands.AddIntervento
{
    public class AddInterventoCommandHandler : ICommandHandler<AddInterventoCommand>
    {
        private readonly ISaveRichiestaAssistenza _saveRichiestaAssistenza;
        private readonly IGetMaxCodice _iGetMaxCodice;

        public AddInterventoCommandHandler(ISaveRichiestaAssistenza saveRichiestaAssistenza, IGetMaxCodice iGetMaxCodice)
        {
            this._saveRichiestaAssistenza = saveRichiestaAssistenza;
            this._iGetMaxCodice = iGetMaxCodice;
        }

        public void Handle(AddInterventoCommand command)
        {
            command.Chiamata.Codice = command.Chiamata.Operatore.Sede.Codice.Split('.')[0] + "-" + (_iGetMaxCodice.GetMax() + 1).ToString();

            var richiesta = new RichiestaAssistenza()
            {
                Tipologie = command.Chiamata.Tipologie,
                ZoneEmergenza = command.Chiamata.ZoneEmergenza,
                Operatore = command.Chiamata.Operatore,
                Richiedente = command.Chiamata.Richiedente,
                Localita = command.Chiamata.Localita,
                Descrizione = command.Chiamata.Descrizione,
                Codice = command.Chiamata.Codice,
                TurnoInserimentoChiamata = command.Chiamata.TurnoInserimentoChiamata,
                TipoTerreno = command.Chiamata.TipoTerreno,
                ListaEntiIntervenuti = command.Chiamata.ListaEntiIntervenuti,
                CodiceObiettivoRilevante = command.Chiamata.CodiceObiettivoRilevante,
                Id = (_iGetMaxCodice.GetMax() + 1).ToString(), //TODO DA TOGLIERE CON LA VERSIONE DB
            };

#warning gestire la logica sulla rilevanza, che è cambiata.

            if (command.Chiamata.IstantePresaInCarico != null)
            {
                new InizioPresaInCarico(richiesta, command.Chiamata.IstantePresaInCarico.Value, command.Chiamata.Operatore.Id);
            }

            if (command.Chiamata.Stato == 4)
            {
                new ChiusuraRichiesta("", richiesta, command.Chiamata.IstanteRicezioneRichiesta, command.Chiamata.Operatore.Id);
            }

            if (command.Chiamata.Etichette != null)
            {
                foreach (var t in command.Chiamata.Etichette)
                {
                    richiesta.Tags.Add(t);
                }
            }

            new Telefonata(richiesta, command.Chiamata.Richiedente.Telefono, command.Chiamata.IstanteRicezioneRichiesta, command.Chiamata.Operatore.Id)
            {
                CognomeChiamante = command.Chiamata.Richiedente.Cognome,
                NomeChiamante = command.Chiamata.Richiedente.Nome,
                RagioneSociale = command.Chiamata.Richiedente.RagioneSociale,
                Motivazione = command.Chiamata.Descrizione,
                NotePrivate = command.Chiamata.NotePrivate,
                NotePubbliche = command.Chiamata.NotePubbliche,
                NumeroTelefono = command.Chiamata.Richiedente.Telefono,
                Esito = command.Chiamata.Azione.ToString(),
            };

            this._saveRichiestaAssistenza.Save(richiesta);
        }
    }
}
