import { Component } from '@angular/core';
import { ProductService }from './sanpham.service'
import { Router } from '@angular/router';
@Component({
  selector: 'app-sanpham',
  templateUrl: './sanpham.component.html',
})
export class SanPhamComponent {
  text: any;
  isLogin: boolean = false;

  constructor(
    private proService: ProductService,
    private router: Router,
  ){}

  DatHang(){
    this.proService.OrderDetail(5).subscribe(
      result =>{
        this.text =result.data;
        this.isLogin = true;
       },
      err =>{
        console.log(err);
      }
    
      
    );

    this.router.navigate(['/trangchu/dathang']);

  }


}
