import { Component, OnInit } from '@angular/core';
import { ProductService }from './sanpham.service'
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
@Component({
  selector: 'app-sanpham',
  templateUrl: './sanpham.component.html',
})
export class SanPhamComponent implements OnInit{
  text: any;
  public productId;
  isLogin: boolean = false;

  constructor(
    private proService: ProductService,
    private router: Router,
    private route: ActivatedRoute,
  ){}
  
  ngOnInit(){
    this.route.paramMap.subscribe((params : ParamMap)=>
    {
      let id = parseInt(params.get('id'));
      this.productId = id;
    });
  }

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
