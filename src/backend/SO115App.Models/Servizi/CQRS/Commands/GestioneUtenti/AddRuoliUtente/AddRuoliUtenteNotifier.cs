﻿//-----------------------------------------------------------------------
// <copyright file="AddRuoliUtenteNotifier.cs" company="CNVVF">
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
using CQRS.Commands.Notifiers;
using SO115App.Models.Servizi.Infrastruttura.Notification.GestioneUtenti.GestioneRuoli;

namespace SO115App.Models.Servizi.CQRS.Commands.GestioneUtenti.AddRuoliUtente
{
    /// <summary>
    ///   classe che notifica l'aggiunta del ruolo ad un utente
    /// </summary>
    public class AddRuoliUtenteNotifier : ICommandNotifier<AddRuoliUtenteCommand>
    {
        private readonly INotifyAddRuoli _sender;

        public AddRuoliUtenteNotifier(INotifyAddRuoli sender)
        {
            _sender = sender;
        }

        /// <summary>
        ///   metodo della classe
        /// </summary>
        /// <param name="command">Il command con i paramentri</param>
        public void Notify(AddRuoliUtenteCommand command)
        {
            _sender.Notify(command);
        }
    }
}
