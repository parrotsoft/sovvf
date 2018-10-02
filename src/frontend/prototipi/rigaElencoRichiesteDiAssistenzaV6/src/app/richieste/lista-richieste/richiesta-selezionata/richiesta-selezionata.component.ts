import { Component, OnInit, Input } from '@angular/core';
import { SintesiRichiesta } from '../../../shared/model/sintesi-richiesta.model';
import { transition, animate, style, state, trigger } from '@angular/animations';

@Component({
  selector: 'app-richiesta-selezionata',
  templateUrl: './richiesta-selezionata.component.html',
  styleUrls: ['./richiesta-selezionata.component.css'],
  animations: [
    trigger('richiestaSelezionata', [
      state('selected', style({
        opacity: 1,
        transform: 'scale(1)'
      })),
      state('not-selected', style({
        opacity: 0,
        transform: 'scale(1.3)'
      })),
      transition('* => selected', animate('200ms ease-in')),
      transition('* => not-selected', animate('200ms ease-out'))
    ]),
  ]
})
export class RichiestaSelezionataComponent implements OnInit {
  @Input() richiestaSelezionata: SintesiRichiesta;
  @Input() richiestaSelezionataState: string;

  constructor() { }

  ngOnInit() {
  }
}
