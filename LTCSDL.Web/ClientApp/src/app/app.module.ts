import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
 import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './TrangChu/home/home.component';
import {TrangChuComponent} from './TrangChu/trangchu.component';
import { SanPhamComponent } from './TrangChu/sanpham/sanpham.component';
import { LoginComponent } from './TrangChu/login/login.component';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TrangChuComponent,
    SanPhamComponent,
    LoginComponent
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'trangchu', component: TrangChuComponent, children: [
        { path: 'home', component: HomeComponent },
        { path: 'thongtinsanpham',component: SanPhamComponent},
        { path: 'login', component: LoginComponent},
        { path: '', redirectTo: 'home', pathMatch: 'full' },

      ]},

      
      { path: '', redirectTo: 'trangchu', pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
