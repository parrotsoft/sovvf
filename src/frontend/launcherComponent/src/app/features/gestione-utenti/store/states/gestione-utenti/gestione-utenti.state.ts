import { Action, Selector, State, StateContext, Store } from '@ngxs/store';
import { GestioneUtente } from '../../../../../shared/interface/gestione-utente.interface';
import {
    AddUtente,
    UpdateUtenteGestione,
    GetUtentiGestione,
    RemoveUtente,
    SetUtentiGestione,
    OpenModalRemoveUtente, GetUtenteDetail, SetUtenteDetail, ClearUtenteDetail
} from '../../actions/gestione-utenti/gestione-utenti.actions';
import { GestioneUtentiService } from '../../../../../core/service/gestione-utenti-service/gestione-utenti.service';
import { ShowToastr } from '../../../../../shared/store/actions/toastr/toastr.actions';
import { ToastrType } from '../../../../../shared/enum/toastr';
import { insertItem, patch, updateItem } from '@ngxs/store/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgZone } from '@angular/core';
import { ConfirmModalComponent } from '../../../../../shared';
import { RicercaUtentiState } from '../ricerca-utenti/ricerca-utenti.state';
import { PatchPagination } from '../../../../../shared/store/actions/pagination/pagination.actions';
import { ResponseInterface } from '../../../../../shared/interface/response.interface';
import { TreeviewSelezione } from '../../../../../shared/model/treeview-selezione.model';

export interface GestioneUtentiStateModel {
    listaUtenti: GestioneUtente[];
    utenteDetail: GestioneUtente;
    nuovoUtenteForm: {
        model?: {
            utente: string;
            ruolo: string;
            sedi: TreeviewSelezione[]
        };
        dirty: boolean;
        status: string;
        errors: any;
    };
}

export const GestioneUtentiStateModelDefaults: GestioneUtentiStateModel = {
    listaUtenti: [],
    utenteDetail: null,
    nuovoUtenteForm: {
        model: undefined,
        dirty: false,
        status: '',
        errors: {}
    }
};

@State<GestioneUtentiStateModel>({
    name: 'gestioneUtenti',
    defaults: GestioneUtentiStateModelDefaults
})
export class GestioneUtentiState {

    constructor(private _gestioneUtenti: GestioneUtentiService,
                private modalService: NgbModal,
                private store: Store,
                private ngZone: NgZone) {
    }

    @Selector()
    static listaUtenti(state: GestioneUtentiStateModel) {
        return state.listaUtenti;
    }

    @Selector()
    static utenteDetail(state: GestioneUtentiStateModel) {
        return state.utenteDetail;
    }

    @Selector()
    static sedeSelezionata(state: GestioneUtentiStateModel) {
        return state.nuovoUtenteForm.model.sedi;
    }

    @Action(GetUtentiGestione)
    getGestioneUtenti({ dispatch }: StateContext<GestioneUtentiStateModel>) {
        const filters = {
            search: this.store.selectSnapshot(RicercaUtentiState.ricerca)
        };
        this._gestioneUtenti.getUtenti(filters).subscribe((response: ResponseInterface) => {
            dispatch(new SetUtentiGestione(response.dataArray));
            dispatch(new PatchPagination(response.pagination));
        });
    }

    @Action(SetUtentiGestione)
    setUtentiGestione({ patchState }: StateContext<GestioneUtentiStateModel>, action: SetUtentiGestione) {
        patchState({
            listaUtenti: action.utenti
        });
    }

    @Action(GetUtenteDetail)
    getUtenteDetail({ dispatch }: StateContext<GestioneUtentiStateModel>, action: GetUtenteDetail) {
        this._gestioneUtenti.getUtente(action.id).subscribe((utente: GestioneUtente) => {
            if (utente) {
                dispatch(new SetUtenteDetail(utente));
            }
        });
    }

    @Action(SetUtenteDetail)
    setUtenteDetail({ patchState }: StateContext<GestioneUtentiStateModel>, action: SetUtenteDetail) {
        patchState({
            utenteDetail: action.utente
        });
    }

    @Action(ClearUtenteDetail)
    clearUtenteDetail({ patchState }: StateContext<GestioneUtentiStateModel>) {
        patchState({
            utenteDetail: null
        });
    }

    @Action(UpdateUtenteGestione)
    updateUtenteGestione({ dispatch }: StateContext<GestioneUtentiStateModel>, action: UpdateUtenteGestione) {
        this._gestioneUtenti.updateUtente(action.utente).subscribe((utente: GestioneUtente) => {
            if (utente) {
                patch(
                    updateItem(+utente.id, utente)
                );
                dispatch(new ShowToastr(ToastrType.Info, 'Ruolo Utente Aggiornato', 'Ruolo utente aggiornato con successo.', 2));
            }
        });
    }

    @Action(AddUtente)
    addUtente({ dispatch }: StateContext<GestioneUtentiStateModel>, action: AddUtente) {
        this._gestioneUtenti.addUtente(action.utente).subscribe((utente: GestioneUtente) => {
            if (utente) {
                patch(
                    insertItem(utente)
                );
                dispatch(new ShowToastr(ToastrType.Info, 'Utente Aggiunto', 'Utente aggiunto con successo.', 3));
            }
        });
    }

    @Action(OpenModalRemoveUtente)
    openModalRemoveUtente({ dispatch }: StateContext<GestioneUtentiStateModel>, action: OpenModalRemoveUtente) {
        this.ngZone.run(() => {
            const modalConfermaAnnulla = this.modalService.open(ConfirmModalComponent, { backdropClass: 'light-blue-backdrop', centered: true });
            modalConfermaAnnulla.componentInstance.icona = { descrizione: 'trash', colore: 'danger' };
            modalConfermaAnnulla.componentInstance.titolo = 'Elimina permesso utente';
            modalConfermaAnnulla.componentInstance.messaggioAttenzione = 'Sei sicuro di voler eliminare questo utente dai permessi?';
            modalConfermaAnnulla.componentInstance.bottoni = [
                { type: 'ko', descrizione: 'Annulla', colore: 'danger' },
                { type: 'ok', descrizione: 'Conferma', colore: 'dark' },
            ];
            modalConfermaAnnulla.result.then(
                (val) => {
                    switch (val) {
                        case 'ok':
                            dispatch(new RemoveUtente(action.id));
                            break;
                        case 'ko':
                            // console.log('Azione annullata');
                            break;
                    }
                    // console.log('Modal chiusa con val ->', val);
                },
                (err) => console.error('Modal chiusa senza bottoni. Err ->', err)
            );
        });
    }

    @Action(RemoveUtente)
    removeUtente({ dispatch }: StateContext<GestioneUtentiStateModel>, action: RemoveUtente) {
        this._gestioneUtenti.removeUtente(action.id).subscribe(() => {
            patch(
                insertItem(u => u.id === action.id)
            );
            dispatch(new ShowToastr(ToastrType.Info, 'Utente Rimosso', 'Utente rimosso con successo.', 3));
        });
    }
}
