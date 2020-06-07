import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {DatHangService} from './dathang.service'



@Component({
  selector: 'app-dat-hang',
  templateUrl: './dathang.component.html',
  styleUrls: ['./dathang.component.css']
})
export class DatHangComponent implements OnInit{

  proID: any;
  
  
  products: Array<any> = this.orderService.getProductsBeforOrder();
  productData: any = {
    id: 0,
    catelogId: 0,
    productname: "",
    price: 0,
    description: "",
    productcontent: "",
    productInventory: 0,
    productImgLink: "",
    catelog: null,
    order: []
  };

  ngOnInit(){
    // this.route.paramMap.subscribe((params : ParamMap)=>
    // {
    //   let id = parseInt(params.get('proId'));
    //   this.proID = id;

    // });
  }
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private orderService : DatHangService,
  ){
    this.route.paramMap.subscribe((params : ParamMap)=>
    {
      let id = parseInt(params.get('proId'));
      this.proID = id;

    });
    this.findProductByID(this.proID)

  }
  BuyAnotherProduct(){

    this.router.navigate['/trangchu/home'];
    
  }
  
  
   findProductByID(productID){
    this.orderService.findProductByID(productID).subscribe(
      result =>{
        if(result.success && result.data != null){
          this.productData = result.data;
          this.products = this.orderService.getProductsAfterOrder(result.data);
        } 
        else{
          alert("something wrong")
        }
      },
      err =>{
          alert(err);
      }
    )
  }




}
