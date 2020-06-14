import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
 import { RouterModule} from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './TrangChu/home/home.component';
import {TrangChuComponent} from './TrangChu/trangchu.component';
import { SanPhamComponent } from './TrangChu/sanpham/sanpham.component';
import { AuthGuard } from './helpers/auth.guard';
import { AuthInterceptor } from './helpers/auth.interceptor';
import {DatHangComponent} from './TrangChu/dathang/dathang.component';
import {AdminComponent} from './admin/admin.component';
import {AuthAdminGuard} from './helpers/auth-admin.guard'
import {PageNotFoundComponent} from './pagenotfound/pagenotfound.component'
import{ChangeProfileComponent} from './TrangChu/changeprofile/changeprofile.component'
import {HistoryTransactionComponent} from './TrangChu/historytransaction/historytransaction.component'
import {AdminProductComponent} from './admin/adminproduct/admin-product.component'
import {NavMenuComponent} from './admin/nav-menu/nav-menu.component'
import {CKEditorModule } from 'ckeditor4-angular'
import {AdminCatelogComponent} from  './admin/admin-catelog/admin-catelog.component'
import {AdminUserComponent} from './admin/admin-user/admin-user.component'
import {AdminTransactionComponent} from './admin/admin-transaction/admin-transaction.component'
import {AdminStatisticalComponent} from './admin/statistical/statistical.component'

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TrangChuComponent,
    SanPhamComponent,
    DatHangComponent,
    AdminComponent,
    PageNotFoundComponent,
    ChangeProfileComponent,
    HistoryTransactionComponent,
    AdminProductComponent,
    NavMenuComponent,
    AdminCatelogComponent,
    AdminUserComponent,
    AdminTransactionComponent,
    AdminStatisticalComponent
    
    
    
  ],
  imports: [
    CKEditorModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'trangchu', component: TrangChuComponent, children: [
        { path: 'home/:id', component: HomeComponent },
        { path: 'changeprofile', component: ChangeProfileComponent },
        { path: 'thongtinsanpham/:id',component: SanPhamComponent },
        { path: 'lichsugiaodich', component:HistoryTransactionComponent},
        { path: 'dathang/:id', component: DatHangComponent, canActivate:[AuthGuard]},
        { path: '', redirectTo: 'home/0', pathMatch: 'full' },
        { path :'**', component: PageNotFoundComponent},


      ]},

      {
        path: 'admin', component: AdminComponent, canActivate:[AuthAdminGuard], children:[
          
          {path:'adminproduct', component: AdminProductComponent},
          {path:'admincatelog', component: AdminCatelogComponent},
          {path:'adminuser', component:AdminUserComponent },
          {path:'admintransaction',component:AdminTransactionComponent},
          {path: 'statistical', component: AdminStatisticalComponent},
          {path:'',redirectTo:'adminproduct',pathMatch:'full'},
          { path :'**', component: PageNotFoundComponent},


          
        ]
      },

      { path: '', redirectTo: 'admin', pathMatch: 'full' },
      
    ], {enableTracing: true ,onSameUrlNavigation : "reload"}  )
    //enableTracing: gắn id cho url, onSameUrlNavigation: reload lại trang when navigate url
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
