import { Component, ViewChild } from '@angular/core';
import {  ActivatedRoute,Router,ParamMap } from '@angular/router';
import { HomeService} from './home.service';
import {TokenStorageService} from '/Users/pc/Desktop/LTCSDL-GiuaKy/LTCSDL.Web/ClientApp/src/app/services/token-storage.service'


const PAGE_SIZE = 3;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  userRole : any = this.tokenStorage.getRole();
  ListProduct: [];
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

    ){
    // this.route.paramMap.subscribe((params : ParamMap)=>
    // {
    //   let id = parseInt(params.get('id'));
    //   this.productId = id;
    // });
    this.goToPage(1);
  }

  // getAllproduct(){
  //   this.homeservice.pagination(1,PAGE_SIZE).subscribe(
  //     result => {
  //       if(result != null){
  //         console.log(result.data);
  //         this.ListProduct = result.data.data;
  //         this.totalPage = result.data.totalPage; 
  //         this.page = result.data.page;
  //         this.size = result.data.size;
  //         this.currentPage = 1;
  //       }
  //       else{
  //         alert('Nothing to show()');
  //       }
  //     }
  //   )
  // }

  //Routing tới trang theo theo id sản phẩm
  onSelect(product){
    this.router.navigate(['/trangchu/thongtinsanpham',product.id]);
  }

  //Đi tới trang theo yêu cầu
  goToPage(page){
    this.homeservice.pagination(page,PAGE_SIZE).subscribe(
      result => {
        if(result != null){
          this.ListProduct = result.data.data;
          this.totalPage = result.data.totalPage;
          this.page = result.data.page;
          this.size = result.data.size;
          this.currentPage = page;
        }
        else{
          alert('Nothing to show()');
        }
      }
    )  
  }

  //Đi tới trang trước
  goPreviousPage()
  {
    if(this.currentPage > 1)
    {
      this.goToPage(this.currentPage - 1);
      this.currentPage -=1;
    }
    else{
      alert("You're being in the first page")
    }
  }

  //Đi tới trang kế tiếp
  
  goNextPage()
  {
    if(this.currentPage < this.totalPage){
      this.goToPage(this.currentPage + 1);
      this.currentPage +=1

    }
    else{
      alert("You're being in the last page");
    }
  }

  //Chuyển number sang array để thực hiện vòng lặp for
  counter(i: number) {
    return new Array(i);
}
  
}
