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
  products: any =[]
  Amount : any = []
  userId: number;
  ProIdvsPronum: type[] = []

  totalAmount:number = 0;
  BasketData: any = {
    proid: 0,
    userid: 0,
    productname: "",
    price: 0,
    productInventory: 0,
    productImgLink: "",
    
    
  };

  ngOnInit(){

  }
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private orderService : DatHangService,
  ){
    this.route.paramMap.subscribe((params : ParamMap)=>
    {
      let id = parseInt(params.get('id'));
      this.userId = id;

    });

    this.FindYourBasket(this.userId);
    for(var i = 0; i < this.products.length; i++){
      var product = this.products[i];
      this.totalAmount += (product.price * product.productInventory)
    }
  }
  BuyAnotherProduct(){

    window.history.go(-2);
    
  }
  
  
  
   FindYourBasket(userId){
    this.orderService.findBasketByUserId(userId).subscribe(
      result =>{
        if(result.success && result.data != null){
          this.products = result.data;
          for(var i = 0; i < this.products.length; i++){
            var product = this.products[i];
            this.totalAmount += (product.price * product.productInventory)
            
            this.ProIdvsPronum.push({proId:product.proid,proNum:product.productInventory})
            console.log(this.ProIdvsPronum)
          }
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

  DeleteProductBasket(product){
    this.orderService.deleteProductInBasket(product.userid,product.proid).subscribe(
      result =>{
        if(result.success && result.data != null){
            const index: number = this.ProIdvsPronum.indexOf({proId:product.proid,proNum:product.productInventory});
            window.location.reload();
            if(index != -1){
              this.ProIdvsPronum.splice(index,1)
            }
            
            alert("Delete Success")
          
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
  amount(product){
    return product.price * product.productInventory
  }

  getPay(){
    if(this.products.length > 0){
      this.orderService.createNewTransaction(this.userId,this.totalAmount,this.ProIdvsPronum).subscribe(
      
        result =>{
          if(result.success && result.data.length > 0){
              alert("Đặt Hàng Thành Công");
              this.orderService.deleteBasket(this.userId).subscribe(res=>{}, err=>{});
              this.products = null;
              this.router.navigate(['/trangchu'])
  
          } 
          else{
            alert("something wrong")
          }
        },
        err=>{
          alert("something wrong")
        }
      )
    }
    else{
      alert('khong co san pham nao de dat')
    }
    
  }



}

export interface type{
  proId: number;
  proNum: number;
}
