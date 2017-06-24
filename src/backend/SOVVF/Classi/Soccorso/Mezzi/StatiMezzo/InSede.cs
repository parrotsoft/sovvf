﻿//-----------------------------------------------------------------------
// <copyright file="InSede.cs" company="CNVVF">
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

namespace Modello.Classi.Soccorso.Mezzi.StatiMezzo
{
    /// <summary>
    ///   Presente presso la sede di servizio
    /// </summary>
    public class InSede : IStatoMezzo
    {
        /// <summary>
        ///   Codice identificativo dello stato
        /// </summary>
        public string Codice
        {
            get
            {
                return "InSede";
            }
        }

        /// <summary>
        ///   Indica se il mezzo è disponibile in questo stato
        /// </summary>
        public bool Disponibile
        {
            get
            {
                return true;
            }
        }
    }
}
