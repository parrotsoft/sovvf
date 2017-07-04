import { Componente } from "../componente/componente.model";

/**
 * Modella un mezzo impegnato su una richiesta di assistenza
 */
export class Mezzo {
    constructor(
        /**
         * Codice
         */
        public codice: string,

        /**
         * Descrizione
         */
        public descrizione: string,
        
        /**
         * Genere. Per es. APS, ABP, AS, CA, ecc.
         */
        public genereMezzo: string,

        /**
         * Codice dello stato del mezzo. Per es. inSede, inViaggio, sulPosto, inRientro,
         * istituto, disimpegnato, ecc. Utile a definire il colore del segnale di stato.
         */
        public statoMezzo: string,
        
        /**
         * Testo del segnale di stato.
         */
        public descrizioneStato: string,

        /**
         * Codice dello stato di efficienza del mezzo. Utile a definire il colore
         * della segnalazione dello stato di efficienza. (per es. inManutenzione, mediocre, buono, ottimo)
         */
        public statoEfficienzaMezzo: string,

        /**
         * Testo dello stato di efficienza
         */
        public descrizioneStatoEfficienza: string,

        /**
         * Codice del livello di carburante. Utile a definire il colore
         * della segnalazione sul livello di carburante. (per es. basso, medio, alto)
         */
        public livelloCarburante: string,

        /**
         * Testo del livello di carburante.
         */
        public descrizioneLivelloCarburante: string,

        /**
         * Codice del livello di estinguente. Utile a definire il colore
         * della segnalazione del livello di estinguente. (per es. basso, medio, alto)
         */
        public livelloEstinguente: string,

        /**
         * Testo della segnalazione sul livello di estinguente
         */
        public descrizioneLivelloEstinguente: string,

        /**
         * Codice dello stato di appartenenza del mezzo. Per es. proprio,
         * altraSede, ecc. Utile a definire il colore della segnalazione
         * sullo stato di appartenenza.
         */
        public appartenenzaMezzo: string,

        /**
         * Testo della segnalazione sullo stato di appartenenza.
         */
        public descrizioneAppartenenzaMezzo: string,

        /**
         * Elenco dei componenti presenti sul mezzo
         */
        public equipaggio: Componente[],

        /**
         * Elenco delle notifiche legate al mezzo
         */
        public notificheMezzo: string[]
    ) {}
}