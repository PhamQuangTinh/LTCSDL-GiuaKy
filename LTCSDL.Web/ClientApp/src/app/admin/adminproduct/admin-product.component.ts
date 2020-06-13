import { Component, OnInit } from '@angular/core';
import {AdminProductService} from './admin-product.service'
declare var $:any;

const PAGE_SIZE = 5;

@Component({
  selector: 'app-admin-product',
  templateUrl: './admin-product.component.html',
  styleUrls: ['./admin-product.component.css'],
})
export class AdminProductComponent implements OnInit {

  ListProduct : []

  product : any = {
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
  totalPage: number;
  page: number;
  size: number;
  currentPage : number;
  isSuccess : boolean = false;

  isEdit : boolean;

  constructor(
    private aProductService :AdminProductService,
  ){}
  
  ngOnInit(){
    this.goToPage(1);
  }

  goToPage(page){
    this.aProductService.pagination(page,PAGE_SIZE).subscribe(
      res =>
      {
        if(res.success && res.data != null)
        {
          this.ListProduct = res.data.data;
          this.totalPage = res.data.totalPage;
          this.page = res.data.page;
          this.size = res.data.size;
          this.currentPage = page;
          this.isSuccess = true;
          
        }else{
          alert("Nothing to Show")
        }
      },
      err =>{
        alert("Something Wrong")
        this.isSuccess = false;
      }
    )
  }


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


  counter(i: number) {
    return new Array(i);
  }

  openModal(isEdit, product){
    this.isEdit = isEdit;
    
    this.product = product;
    
    
    $('#addNewProduct').modal('show')
  }
  
}
