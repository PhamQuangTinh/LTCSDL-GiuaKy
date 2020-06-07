import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
 import { RouterModule} from '@angular/router';
 import {LocationStrategy, HashLocationStrategy} from '@angular/common';

import { AppComponent } from './app.component';
import { HomeComponent } from './TrangChu/home/home.component';
import {TrangChuComponent} from './TrangChu/trangchu.component';
import { SanPhamComponent } from './TrangChu/sanpham/sanpham.component';
import { AuthGuard } from './helpers/auth.guard';
import { UserService } from './services/user.service';
import { AuthInterceptor } from './helpers/auth.interceptor';
import {DatHangComponent} from './TrangChu/dathang/dathang.component';
import {AdminComponent} from './admin/admin.component';
import {NewProductComponent} from './admin/new-product/new-product.component'
import {AuthAdminGuard} from './helpers/auth-admin.guard'
import {PageNotFoundComponent} from './pagenotfound/pagenotfound.component'


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TrangChuComponent,
    SanPhamComponent,
    DatHangComponent,
    AdminComponent,
    NewProductComponent,
    PageNotFoundComponent,
    
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'trangchu', component: TrangChuComponent, children: [
        { path: 'home', component: HomeComponent },
        { path: 'thongtinsanpham/:id',component: SanPhamComponent },
        { path: 'dathang/:id', component: DatHangComponent, canActivate:[AuthGuard]},
        { path: '', redirectTo: 'home', pathMatch: 'full' },
        { path :'**', component: PageNotFoundComponent},


      ]},

      {
        path: 'admin', component: AdminComponent, canActivate:[AuthAdminGuard], children:[
          {path:'new-product', component: NewProductComponent},
          
        ]
      },

      { path: '', redirectTo: 'trangchu', pathMatch: 'full' },
      
    ], {enableTracing: true} )
  ],
  providers: [
    // UserService,
    // {
    //   provide: LocationStrategy,
    //   useClass: HashLocationStrategy, 
    // },

    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
