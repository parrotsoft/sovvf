import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { GooglePlaceModule } from 'ngx-google-places-autocomplete';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ClipboardModule } from 'ngx-clipboard';
import { ReactiveFormsModule } from '@angular/forms';
import { ChiamataComponent } from './chiamata.component';
import { SchedaTelefonataComponent } from './scheda-telefonata/scheda-telefonata.component';
import { ChiamataService } from '../../../core/service/chiamata-service/chiamata.service';
import { ChiamataServiceFake } from '../../../core/service/chiamata-service/chiamata.service.fake';
import { NgxsModule } from '@ngxs/store';
import { ChiamataState } from './store/states/chiamata.state';

@NgModule({
    imports: [
        CommonModule,
        NgSelectModule,
        FormsModule,
        GooglePlaceModule,
        NgbModule,
        ClipboardModule,
        ReactiveFormsModule,
        NgxsModule.forFeature([
            ChiamataState
        ])
    ],
    declarations: [
        ChiamataComponent,
        SchedaTelefonataComponent
    ],
    exports: [
        ChiamataComponent
    ],
    providers: [{
        provide: ChiamataService, useClass: ChiamataServiceFake
    }]
})
export class ChiamataModule {
}
