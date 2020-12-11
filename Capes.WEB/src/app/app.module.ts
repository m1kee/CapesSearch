import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgxPaginationModule } from 'ngx-pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
/* Routing */
import { APP_ROUTING } from '@app/app.routing';

import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from '@app/services/auth.service';
import { SearchComponent } from './components/search/search.component';
import { SearchService } from '@app/services/search.service';

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        SearchComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        BrowserAnimationsModule,
        HttpClientModule,
        FontAwesomeModule,
        APP_ROUTING,
        NgxPaginationModule,
        ToastrModule.forRoot({
            closeButton: true,
            timeOut: 2500,
            extendedTimeOut: 1000,
            disableTimeOut: false,
            easing: 'ease-in',
            easeTime: 300,
            enableHtml: false,
            progressBar: true,
            progressAnimation: 'decreasing',
            toastClass: 'toast',
            positionClass: 'toast-top-center',
            titleClass: 'toast-title',
            messageClass: 'toast-message',
            tapToDismiss: true,
            onActivateTick: false
        }),
    ],
    providers: [AuthService, SearchService],
    bootstrap: [AppComponent]
})
export class AppModule { }
