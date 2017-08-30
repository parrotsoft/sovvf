﻿//-----------------------------------------------------------------------
// <copyright file="InRientro.cs" company="CNVVF">
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
using System;
using System.Diagnostics.CodeAnalysis;
using Modello.Classi.Soccorso.Eventi.Eccezioni;
using Modello.Classi.Soccorso.Eventi.Partenze;

namespace Modello.Classi.Soccorso.Mezzi.StatiMezzo
{
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "https://stackoverflow.com/questions/37189518/stylecop-warning-sa1126prefixcallscorrectly-on-name-of-class")]

    /// <summary>
    ///   In viaggio verso la sede di servizio
    /// </summary>
    public class InRientro : AbstractStatoMezzo
    {
        /// <summary>
        ///   In questo stato il mezzo risulta assegnato ad una richiesta
        /// </summary>
        public override bool AssegnatoARichiesta
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///   Codice identificativo dello stato
        /// </summary>
        public override string Codice
        {
            get
            {
                return nameof(InRientro);
            }
        }

        /// <summary>
        ///   In questo stato il mezzo risulta disponibile per l'assegnazione
        /// </summary>
        public override bool Disponibile
        {
            get
            {
                return true;
            }
        }

        public override IStatoMezzo AcceptVisitor(PartenzaRientrata partenzaRientrata)
        {
            return new InSede();
        }

        public override IStatoMezzo AcceptVisitor(ComposizionePartenze composizionePartenze)
        {
            throw new WorkflowException();
        }

        public override IStatoMezzo AcceptVisitor(PartenzaInRientro partenzaInRientro)
        {
            throw new WorkflowException();
        }

        public override IStatoMezzo AcceptVisitor(UscitaPartenza fuoriServizio)
        {
            throw new WorkflowException();
        }

        public override IStatoMezzo AcceptVisitor(Revoca revoca)
        {
            return new Libero();
        }

        public override IStatoMezzo AcceptVisitor(VaInFuoriServizio vaInFuoriServizio)
        {
            throw new WorkflowException();
        }

        public override IStatoMezzo AcceptVisitor(ArrivoSulPosto arrivoSulPosto)
        {
            throw new WorkflowException();
        }
    }
}
