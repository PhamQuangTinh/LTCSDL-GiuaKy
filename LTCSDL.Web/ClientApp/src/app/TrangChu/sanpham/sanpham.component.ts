import { Component, OnInit } from '@angular/core';
import { ProductService }from './sanpham.service'
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {TokenStorageService} from '/Users/pc/Desktop/LTCSDL-GiuaKy/LTCSDL.Web/ClientApp/src/app/services/token-storage.service'
declare var $:any;

@Component({
  selector: 'app-sanpham',
  templateUrl: './sanpham.component.html',
  styleUrls: ['./sanpham.component.css'],

})
export class SanPhamComponent implements OnInit{
  text: any;
  productId : number;
  isLogin: boolean = false;
  userId : number; 
  soluongdathang: number;
  amount: any;
  
  productImfo: any = {
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
  }

  listComment = [];
  comment: any ={
      id: 0,
      userId: 0,
      userName: "",
      productId: 0,
      commentContent: "",
      timeComment: null
  }
    
  ngOnInit(){
    //trả về trang có id = ?
    this.route.paramMap.subscribe((params : ParamMap)=>
    {
      let id = parseInt(params.get('id'));
      this.productId = id;
    });
    this.ThongTinSanPham(this.productId);
    this.GetCommentProdcut(this.productId);
    this.userId = this.tokenStorage.getUser().id
  }

  constructor(
    private proService: ProductService,
    private router: Router,
    private route: ActivatedRoute,
    private tokenStorage: TokenStorageService,
  ){
    
  }


  ThongTinSanPham(productId){
    
    this.proService.FindProductById(productId).subscribe(
      result => {
        if(result.success && result.data != null){
          this.productImfo = result.data;
        }
        else{
          alert("can't find this product")          
        }
        
      },
      err =>{
        alert(err);
      }
    );
    
  }

  GetCommentProdcut(productId){
    this.proService.GetCommentProduct(productId).subscribe(
      result =>{
          if(result.success && result.data != null){
              this.listComment = result.data;
          }
          else{
            alert("Failed to load comment")
          }
      },
      err =>{
        alert(err);
      }
    )

  } 

  CreateComment(comment:string){

    if(this.tokenStorage.getUser().id != 0 && comment != ""){
      this.userId = this.tokenStorage.getUser().id
      this.proService.CreateNewComment(this.userId, this.productId, comment).subscribe(
        res =>{
          if(res.success && res.data != null){
              this.GetCommentProdcut(this.productId);
              $('#exampleFormControlTextarea1').val('');
          }
          else{
            alert("Something Wrong")
          }
        }
      )
    }
    else if(this.tokenStorage.getUser().id == 0){
      alert("Bạn phải đăng nhập trước khi comment");
    }else if(comment == ""){
      alert("Hãy nhập nội dung")
    }
  }

  goback(){
    window.history.back();
  }

  dathang(){
      if(this.productId > 0 && this.soluongdathang > 0){
        this.amount = this.soluongdathang * this.productImfo.price;
      this.proService.CreateNewTransaction(this.userId,this.amount,this.productId,this.soluongdathang).subscribe(
        res =>{
          if(res.success && res.data != null){
            this.router.navigate(['/trangchu/dathang',this.userId,{proId: this.productImfo.id}]);
          }
          else{
            alert("data null")
          }
          
        },err =>{
          alert("something wrong")
        }
      );
    }else{
      alert("Nhap so luong san pham")
    }
        
    
  }

  // DatHang(){
  //   this.proService.OrderDetail(5).subscribe(
  //     result =>{
  //       result.
  //       this.text =result.data;
  //       this.isLogin = true;
  //      },
  //     err =>{
  //       console.log(err);
  //     }
    
      
  //   );

  //   this.router.navigate(['/trangchu/dathang']);

  // }


}
