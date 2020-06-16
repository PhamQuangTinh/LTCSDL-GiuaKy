import { Component, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { HomeService } from './home.service';
import {TokenStorageService} from '../services/token-storage.service'
import { AnonymousSubject } from 'rxjs/internal/Subject';
import { Observable } from 'rxjs';


const PAGE_SIZE = 6;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit{
  userRole: any = this.tokenStorage.getRole();
  ListProduct: [];
  Top3Product: Top3[] = [];
  mySentences:Top3[];
  
  //Tu khoa search
  searchKeyword: any;
  lPrice : any;
  fPrice : any;

  ok : Observable<any>;
  categoryId:number;
  totalPage: number;
  page: number;
  size: number;
  currentPage: number;
  product: any = {
    id: 0,
    catelogId: 0,
    productname: "",
    price: "",
    description: "",
    productcontent: "",
    productInventory: 0,
    productImgLink: "",
    catelog: null,
    order: []
  }
  constructor(
    private router: Router, private route: ActivatedRoute,
    private homeservice: HomeService,
    private tokenStorage: TokenStorageService,

  ) {
    // this.route.paramMap.subscribe((params : ParamMap)=>
    // {
    //   let id = parseInt(params.get('id'));
    //   this.productId = id;
    // });
    this.route.paramMap.subscribe((params: ParamMap) => {
      let id = parseInt(params.get('id'));
      this.categoryId = id;
      let fPrice = params.get('fPrice');
      let lPrice = params.get('lPrice'); 
      let key = params.get('keyword');

      if(key != null){
        this.searchKeyword = key;
      }
      else{
        this.searchKeyword = "";
      }

      if(lPrice != null){
        this.lPrice = lPrice;
      }else{
        this.lPrice = '';
      }
      
      if(fPrice != null){
        this.fPrice = fPrice;
      }else{
        this.fPrice = '';
      }


      
    });
    
    
  }

  ngOnInit(){
    this.goToPage(1);
    this.getTop3Product();
    //reload lại trang
    this.router.routeReuseStrategy.shouldReuseRoute = () =>{
      return false;
    }

    


}



  //Routing tới trang theo theo id sản phẩm
  onSelect(product) {
    this.router.navigate(['/trangchu/thongtinsanpham', product.id]);
  }

  //Đi tới trang theo yêu cầu
  goToPage(page) {
    if(this.fPrice == '' && this.lPrice == ''){
      this.ok = this.homeservice.pagination(page, PAGE_SIZE,0,this.categoryId,this.searchKeyword.toString(),0,0);
    }
    else if(this.lPrice != '' || this.fPrice != ''){
      if(this.lPrice == '' && this.fPrice != ''){
        
        this.ok = this.homeservice.pagination(page, PAGE_SIZE,1,this.categoryId,this.searchKeyword,parseFloat(this.fPrice),99999);
      }
      else if(this.lPrice != '' && this.fPrice == '')
      {
        this.ok = this.homeservice.pagination(page, PAGE_SIZE,1,this.categoryId,this.searchKeyword,0,parseFloat(this.lPrice));
      }
      else{
        this.ok = this.homeservice.pagination(page, PAGE_SIZE,1,this.categoryId,this.searchKeyword,parseFloat(this.fPrice),parseFloat(this.lPrice));
      }
      
  
    }
    this.ok.subscribe(
      result => {
        if (result != null) {
          this.ListProduct = result.data.data;
          this.totalPage = result.data.totalPage;
          this.page = result.data.page;
          this.size = result.data.size;
          this.currentPage = page;
          
        }
        else {
          alert('Nothing to show');
        }
      },
      err=>{
        alert("Something Wrong")
      }
    )
   
  }

  //Đi tới trang trước
  goPreviousPage() {
    if (this.currentPage > 1) {
      this.goToPage(this.currentPage - 1);
      this.currentPage -= 1;
    }
    else {
      alert("You're being in the first page")
    }
  }

  //Đi tới trang kế tiếp

  goNextPage() {
    if (this.currentPage < this.totalPage) {
      this.goToPage(this.currentPage + 1);
      this.currentPage += 1

    }
    else {
      alert("You're being in the last page");
    }
  }

  //Chuyển number sang array để thực hiện vòng lặp for
  counter(i: number) {
    return new Array(i);
  }

  getTop3Product() {
    this.homeservice.getTop3Product().subscribe(
      res => {
        this.Top3Product = res.data
        console.log(this.Top3Product)
      },
      err => {
        alert("Something wrong");
      }
    )
  }

}

export interface Top3{
      productId: number,
      productImgLink: any,
      rank: number,
}
