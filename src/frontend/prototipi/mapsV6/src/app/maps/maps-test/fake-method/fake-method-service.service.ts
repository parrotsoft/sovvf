import {Injectable} from '@angular/core';
import {RichiestaMarker} from '../../maps-model/richiesta-marker.model';
import {Subscription} from 'rxjs';
import {MarkedService} from '../../service/marked-service/marked-service.service';
import {MapManagerService} from '../../service/maps-manager/map-manager-service.service';
import {CenterService} from '../../service/center-service/center-service.service';
import {Localita} from '../../../shared/model/localita.model';
import {Coordinate} from '../../../shared/model/coordinate.model';
import {Tipologia} from '../../../shared/model/tipologia.model';
import {DispatcherService} from '../../dispatcher/dispatcher.service';

@Injectable({
    providedIn: 'root'
})
export class FakeMethodService {

    private stati: any;
    private statiObj: any;

    markerSelezionato: any;
    selezione: Subscription;

    constructor(private markedService: MarkedService,
                private mapManager: MapManagerService,
                private dispatcher: DispatcherService,
                private centerService: CenterService) {
        this.selezione = this.markedService.getMarked().subscribe(marker => {
            this.markerSelezionato = marker;
        });
        this.stati = [
            [1, 'chiamata'],
            [2, 'assegnato'],
            [3, 'presidiato'],
            [4, 'sospeso']
        ];
        this.statiObj = new Map(this.stati);
    }

    /* TESTING METHOD */
    setMarker(marker: RichiestaMarker) {
        this.dispatcher.addMarker(marker);
    }

    /* TESTING METHOD */
    setRandomMarker() {
        this.mapManager.count++;
        const randomNumber = Math.floor(Math.random() * 4) + 1;
        const lat = Math.floor(Math.random() * 10) * 0.1 + 41.89;
        const long = Math.floor(Math.random() * 10) * 0.1 + 12.49;
        const markerRandom = new RichiestaMarker(
            'R' + this.mapManager.count,
            new Localita(new Coordinate(lat, long), 'Via Cavour, 5'),
            [
                new Tipologia(1, 'allagamento', '')
            ],
            'Marker Random: ' + this.statiObj.get(randomNumber),
            false,
            Math.floor(Math.random() * 5) + 1,
            this.statiObj.get(randomNumber));
        this.setMarker(markerRandom);
    }

    /* TESTING METHOD */
    removeLastMarker() {
        if (this.markerSelezionato &&
            this.mapManager.richiesteMarker.slice(-1).pop().id === this.markerSelezionato.id) {
            this.markedService.clearMarked();
        }
        this.mapManager.richiesteMarker.pop();
    }

    /* TESTING METHOD */
    changeMarkerColor() {
        const statoCasuale = Math.floor(Math.random() * 4) + 1;
        const color = this.mapManager.richiesteMarker.find(x => x.id === this.markerSelezionato.id);
        color.stato = this.statiObj.get(statoCasuale);
        // this.dispatcher.updateMarker(color);
        this.markedService.clearMarked();
    }


    /* TESTING METHOD */
    changeMarkerSize() {
        const size = this.mapManager.richiesteMarker.find(x => x.id === this.markerSelezionato.id);
        if (size.priorita > 0 && size.priorita < 5) {
            size.priorita++;
        } else if (size.priorita === 5) {
            size.priorita = 1;
        }
        // this.dispatcher.updateMarker(size);
        this.markedService.clearMarked();
    }

    /* TESTING METHOD */
    changeMarkerAnimation() {
        /**
         * metodo da rivedere con gmaps...
         */
        // const statoCasuale = Math.floor(Math.random() * 4) + 1;
        // console.log(statoCasuale);
        console.log('modifico animazione al marker selezionato');
    }

    /* TESTING METHOD */
    removeMarker() {
        const remove = this.mapManager.richiesteMarker.find(x => x.id === this.markerSelezionato.id);
        this.dispatcher.deleteMarker(remove);
        this.markedService.clearMarked();
    }

    aggiornaCentro(centro) {
        this.centerService.clearCentro();
        this.centerService.sendCentro(centro);
    }

}
