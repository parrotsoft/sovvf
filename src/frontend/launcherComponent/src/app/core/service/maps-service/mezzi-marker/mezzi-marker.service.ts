import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { handleError } from '../../../../shared/helper/handleError';
import { AreaMappa } from '../../../../features/home/maps/maps-model/area-mappa-model';
import { FiltroMezzi } from '../../../../features/home/maps/maps-model/filtro-mezzi.interface';
import { AreaMappaFiltrata } from '../../../../shared/helper/query-helper';
import { Markers } from '../../../../shared/enum/markers.enum';

const API_MARKER = environment.apiUrl.markers;

@Injectable()
export class MezziMarkerService {

    constructor(private http: HttpClient) {
    }

    public getMezziMarkers(areaMappa: AreaMappa, filtroMezzi?: FiltroMezzi): Observable<any> {
        return this.http.post(`${API_MARKER}/GetMezzi`, AreaMappaFiltrata(areaMappa, filtroMezzi, null, Markers.Mezzi)).pipe(
            retry(3),
            catchError(handleError));
    }

}
