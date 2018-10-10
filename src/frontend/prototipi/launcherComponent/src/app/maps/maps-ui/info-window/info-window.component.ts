import {Component, Input, OnInit} from '@angular/core';
import {Meteo} from '../../../shared/model/meteo.model';
import {RichiestaMarker} from '../../maps-model/richiesta-marker.model';
import {SedeMarker} from '../../maps-model/sede-marker.model';
import {MezzoMarker} from '../../maps-model/mezzo-marker.model';
import {MezzoModalContentComponent} from './mezzo-modal-content/mezzo-modal-content.component';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-info-window',
    templateUrl: './info-window.component.html',
    styleUrls: ['./info-window.component.css']
})
export class InfoWindowComponent implements OnInit {

    @Input() datiMeteo: Meteo;
    @Input() richiestaMarker: RichiestaMarker;
    @Input() sedeMarker: SedeMarker;
    @Input() mezzoMarker: MezzoMarker;

    constructor(private _modalService: NgbModal) {
    }

    openModal() {
        this._modalService.open(MezzoModalContentComponent);
    }

    ngOnInit() {
    }

}