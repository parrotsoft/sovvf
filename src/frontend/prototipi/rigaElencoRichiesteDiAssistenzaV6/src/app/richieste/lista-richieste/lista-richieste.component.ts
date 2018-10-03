import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { SintesiRichiesta } from '../../shared/model/sintesi-richiesta.model';
import { ListaRichiesteManagerService } from '../lista-richieste-manager/lista-richieste-manager.service';
import { RichiestaSelezionataService } from '../lista-richieste-service/richiesta-selezionata-service/richiesta-selezionata-service.service';
import { RichiestaHoverService } from '../lista-richieste-service/richiesta-hover-service/richiesta-hover-service.service';

@Component({
  selector: 'app-lista-richieste',
  templateUrl: './lista-richieste.component.html',
  styleUrls: ['./lista-richieste.component.css']
})
export class ListaRichiesteComponent implements OnInit {
  richieste: SintesiRichiesta[];
  id_richiestaHover: any;
  id_richiestaSelezionata: any;
  richiestaHover: SintesiRichiesta;
  richiestaSelezionata: SintesiRichiesta;
  richiestaSelezionataState: string;

  constructor(private listaRichiesteManager: ListaRichiesteManagerService,
    private richiestaSelezionataS: RichiestaSelezionataService,
    private richiestaHoverS: RichiestaHoverService) { }

  ngOnInit() {
    // Restituisce le Richieste
    this.listaRichiesteManager.getData().subscribe(richieste => {
      this.richieste = richieste;
    });

    // Restituisce la Richiesta Selezionata
    this.richiestaSelezionataS.getRichiesta().subscribe(idRichiesta => {
      if (idRichiesta) {
        this.id_richiestaSelezionata = idRichiesta;
        this.richieste.forEach((r: SintesiRichiesta) => {
          if (r.id === idRichiesta) {
            this.richiestaSelezionata = r;
          }
        });
      } else {
        this.id_richiestaSelezionata = null;
        this.richiestaSelezionata = null;
      }
    });

    // Restituisce la Richiesta Hover
    this.richiestaHoverS.getRichiesta().subscribe(idRichiesta => {
      if (idRichiesta) {
        this.id_richiestaHover = idRichiesta;
        this.richieste.forEach((r: SintesiRichiesta) => {
          if (r.id === idRichiesta) {
            this.richiestaHover = r;
          }
        });
      } else {
        this.richiestaHover = null;
        this.id_richiestaHover = null;
      }
    });
  }
}