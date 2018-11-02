import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { Observable } from 'rxjs/Rx';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTableModule } from '@angular/material';

import { AppComponent } from './components/app/app.component';
import { GrodtComponent } from './components/grodt/grodt.component';
import { CoinService } from './services/coin.service';

@NgModule({
    declarations: [
        AppComponent,
        GrodtComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        MatTableModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'grodt', pathMatch: 'full' },
            { path: 'grodt', component: GrodtComponent },
            { path: '**', redirectTo: 'grodt' }
        ])
    ],
    providers: [CoinService]
})
export class AppModuleShared {
}
