import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { ViewInterface } from '../view-mode/view.interface';

@Injectable({
    providedIn: 'root'
})
export class ViewService {

    viewState: ViewInterface = {
        richieste: true,
        mappa: true,
        comp_partenza: false,
        split: true,
        chiamata: false,
        columns: ['col-6', 'col-6']
    };

    colorButton = ['btn-outline-secondary', 'btn-secondary', 'btn-outline-secondary'];

    private subject = new Subject<ViewInterface>();

    sendView(view: ViewInterface) {
        this.viewState = view;
        this._columns(view);
        this.subject.next(view);
    }

    getView(): Observable<ViewInterface> {
        return this.subject.asObservable();
    }

    _columns(view) {
        if (view.split) {
            view.columns = ['col-6', 'col-6'];
        } else {
            if (view.mappa) {
                view.columns = ['', 'col-12'];
            } else {
                view.columns = ['col-12', ''];
            }
        }
    }
}