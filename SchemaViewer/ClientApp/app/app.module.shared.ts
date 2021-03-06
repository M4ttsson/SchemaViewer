import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
//import { HomeComponent } from './components/home/home.component';
//import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { AboutComponent } from './components/about/about.component';
//import { CounterComponent } from './components/counter/counter.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        //CounterComponent,
        //FetchDataComponent,
        CalendarComponent,
        AboutComponent
        //HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            //{ path: 'home', component: HomeComponent },
            //{ path: 'counter', component: CounterComponent },
            //{ path: 'fetch-data', component: FetchDataComponent },
            { path: 'home', component: CalendarComponent },
            { path: 'about', component: AboutComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}
