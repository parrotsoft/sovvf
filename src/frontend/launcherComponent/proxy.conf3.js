const PROXY_CONFIG = [{
    context: [
        '/NotificationHub',
        '/api/Auth',
        '/api/Welcome',
        '/api/Navbar',
        '/api/Filtri',
        '/api/Chiamata',
        '/api/ComposizionePartenzaAvanzata',
        '/api/PreAccoppiati',
        '/api/ChiamataInCorso',
        '/api/ListaEventi',
        '/api/AddPrenotazioneMezzo',
        '/api/RemovePrenotazioneMezzo',
        '/api/ResetPrenotazioneMezzo',
        '/api/ConfermaPartenze',
        '/api/AttivitaUtente',
        '/api/GestionePartenza',
        '/api/GestioneRichiesta',
        '/api/GestioneMezziInServizio',
        '/api/Marker',
        '/api/PrenotazioneMezzo',
        '/api/GestioneSchedeContatto',
        '/api/GestioneUtenti',
        '/api/GestioneRuolo',
        '/api/PersonaleVVF'
    ],
    target: "http://172.16.16.156:5000/",
    secure: false,
    "changeOrigin": true,
    ws: true
}];
module.exports = PROXY_CONFIG;
