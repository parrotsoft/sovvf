﻿//-----------------------------------------------------------------------
// <copyright file="AddUtenteCommandHandler.cs" company="CNVVF">
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
using SO115App.API.Models.Classi.Autenticazione;
using SO115App.API.Models.Classi.Condivise;
using SO115App.API.Models.Classi.Organigramma;
using SO115App.Models.Servizi.Infrastruttura.GestioneUtenti;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.Personale;
using SO115App.Models.Servizi.Infrastruttura.SistemiEsterni.ServizioSede;
using System.Collections.Generic;

namespace SO115App.Models.Servizi.CQRS.Commands.GestioneUtenti.AddUtente
{
    /// <summary>
    ///   l'handler che si occupa della logica di aggiunta di un utente come operatore
    /// </summary>
    public class AddUtenteCommandHandler : ICommandHandler<AddUtenteCommand>
    {
        private readonly IAddUtente _addUtente;
        private readonly IGetPersonaleByCF _personaleByCF;

        public AddUtenteCommandHandler(IAddUtente addUtente, IGetPersonaleByCF personaleByCF)
        {
            _addUtente = addUtente;
            _personaleByCF = personaleByCF;
        }

        /// <summary>
        ///   metodo della classe
        /// </summary>
        /// <param name="command">il command con i parametri di ingresso</param>
        public void Handle(AddUtenteCommand command)
        {
            var personale = _personaleByCF.Get(command.CodFiscale, command.CodiceSede.Split(".")[0]).Result;

            var utenteVVF = new Utente(command.CodFiscale, personale.Nominativo.Split(".")[0], personale.Nominativo.Split(".")[1])
            {
                Ruoli = command.Ruoli,
                Username = "test",
                Password = "test",
                Sede = new Sede(command.CodiceSede, "", "", new Coordinate(0, 0), "", "", "", "", "")
            };

            _addUtente.Add(utenteVVF);
        }
    }
}