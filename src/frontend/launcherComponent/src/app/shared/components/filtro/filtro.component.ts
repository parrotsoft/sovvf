import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { VoceFiltro } from '../../../features/home/filterbar/ricerca-group/filtri-richieste/voce-filtro.model';

@Component({
  selector: 'app-filtro',
  templateUrl: './filtro.component.html',
  styleUrls: ['./filtro.component.css']
})
export class FiltroComponent implements OnInit {
  @Input() filtro: VoceFiltro;
  @Output() filtroSelezionato: EventEmitter<VoceFiltro> = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  onSelezione(filtro: VoceFiltro) {
      this.filtroSelezionato.emit(filtro);
  }

}
