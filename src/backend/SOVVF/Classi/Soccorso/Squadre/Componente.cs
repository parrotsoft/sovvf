﻿using System.Collections.Generic;

namespace Modello.Classi.Soccorso.Squadre
{
    /// <summary>
    ///   Modella il componente di una partenza, con i suoi ruoli, il mezzo sul quale è collocato e
    ///   le attrezzature che ha in carico.
    /// </summary>
    public class Componente
    {
        /// <summary>
        ///   Indica i possibili ruoli con i quali si partecipa ad una partenza.
        /// </summary>
        public enum Ruolo
        {
            /// <summary>
            ///   E' il ruolo del capoPartenza
            /// </summary>
            CapoPartenza,

            /// <summary>
            ///   E' il ruolo dell'autista
            /// </summary>
            Autista,

            /// <summary>
            ///   E' il ruolo base che non implica mansioni speciali
            /// </summary>
            Vigile
        }

        /// <summary>
        ///   E' il codice fiscale del membro appartenente alla partenza
        /// </summary>
        public string CodiceFiscale { get; set; }

        /// <summary>
        ///   E' la lista dei ruoli assegnati al soggetto contestualmente alla richiesta in corso
        /// </summary>
        public ISet<Ruolo> Ruoli { get; set; }

        /// <summary>
        ///   Per la classe, un Componente è uguale ad un altro Componente se hanno lo stesso codice fiscale
        /// </summary>
        /// <param name="obj">Oggetto da confrontare</param>
        /// <returns>true se il Componente passato è uguale</returns>

        public override bool Equals(object obj)
        {
            if (!(obj is Componente))
            {
                return false;
            }

            var c = (Componente)obj;
            return c.CodiceFiscale.Equals(this.CodiceFiscale);
        }

        /// <summary>
        ///   Implementato hascode che restituisce l'hascode del codice fiscale
        /// </summary>
        /// <returns>Hashcode della classe</returns>
        public override int GetHashCode()
        {
            return this.CodiceFiscale.GetHashCode();
        }
    }
}