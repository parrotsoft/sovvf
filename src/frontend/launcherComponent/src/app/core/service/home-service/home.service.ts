import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError, map, retry, tap } from 'rxjs/operators';
import { handleError } from '../../../shared/helper/handleError';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Welcome } from '../../../shared/interface/welcome.interface';
import { Tipologia } from '../../../shared/model/tipologia.model';

const API_WELCOME = environment.apiUrl.welcome;

@Injectable()
export class HomeService {

    constructor(private http: HttpClient) {
    }

    getHome(): Observable<Welcome> {
        return this.http.get<Welcome>(API_WELCOME).pipe(
            map((data: Welcome) => mapAppSettings(data)),
            retry(3),
            catchError(handleError)
        );

        function mapAppSettings(data: Welcome): Welcome {
            data.tipologie = data.tipologie.map((tipologia: Tipologia) => {
                return {
                    ...tipologia,
                    codiceDescrizione: `${tipologia.descrizione} (${tipologia.codice})`
                };
            });
            return data;
        }
    }
}
