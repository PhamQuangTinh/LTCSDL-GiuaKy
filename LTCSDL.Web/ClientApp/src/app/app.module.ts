import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import {TrangChuComponent} from './TrangChu/trangchu.component';
import { CounterComponent } from './counter/counter.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    TrangChuComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'trangchu', component: TrangChuComponent, children: [
        { path: 'counter', component: CounterComponent },
        { path: 'FetchDataComponent', component: CounterComponent },
        { path: 'fetch-data', component: FetchDataComponent },
        { path: 'home', component: HomeComponent },
        { path: '', redirectTo: 'home', pathMatch: 'full' },

      ]},
      { path: '', redirectTo: 'trangchu', pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
