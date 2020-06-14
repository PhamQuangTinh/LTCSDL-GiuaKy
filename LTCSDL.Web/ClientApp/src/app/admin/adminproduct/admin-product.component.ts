import { Component, OnInit } from '@angular/core';
import {AdminProductService} from './admin-product.service'
import {Observable} from 'rxjs'

declare var $:any;

const PAGE_SIZE = 5;

@Component({
  selector: 'app-admin-product',
  templateUrl: './admin-product.component.html',
  styleUrls: ['./admin-product.component.css'],
})
export class AdminProductComponent implements OnInit {

  ListProduct : []
  
  ListCatelog : []

  ObserResult : Observable<any>;

  productT : any ={
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
  product : any = this.productT
  totalPage: number;
  page: number;
  size: number;
  currentPage : number;
  isSuccess : boolean = false;

  isEdit : boolean;
  isDelete : boolean = false;

  constructor(
    private aProductService :AdminProductService,
  ){}
  
  ngOnInit(){
    this.goToPage(1);
    this.getAllCatelog();
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
        alert("Something Wrong at goToPage()  ")
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
    this.isDelete = false;
    if(product != null){
      this.product = product
      
    }else{
       this.product = this.productT;  
    }    
    $('#addNewProduct').modal('show')
  }

  editComfirm(){
    $('#editComfirm').modal('show')
  }
  deleteconfirm(product){
    this.product = product;

    $('#deleteconfirm').modal('show')
  }

  getAllCatelog(){
    this.aProductService.getAllCatelog().subscribe(
      res =>{
        if(res.data != null){
          this.ListCatelog = res.data;
          this.goToPage(1);
        }else{
          alert("data null")
        }
      },err=>{
        alert("Something wrong at getAllCatelog()")
      }
    )
  }

  editProduct(){
    if(this.isEdit){
      this.ObserResult = this.aProductService.editProduct(this.product);
    }else{
      this.ObserResult = this.aProductService.createNewProduct(this.product);
    }

    this.ObserResult.subscribe(
      res =>{
        if(res.success && res.data != null){
          this.product = res.data;
          if(this.isEdit){
            $('#addNewProduct').modal('hide')
            alert("Sửa thành công")
          }else{
            alert("Thêm mới thành công")
            this.isEdit = true;
          }
          $('#editComfirm').modal('hide')
          this.goToPage(1);
        }
        else{
          $('#editComfirm').modal('hide')

          alert("data null")
        }
      },err=>{
        $('#editComfirm').modal('hide')

        alert("something wrong at editProuct")
      }
    )
    
  }

  deleteProduct(){
    console.log(this.product)
    this.aProductService.removeProduct(this.product).subscribe(
      res =>{
        if(res.data!= null){
          alert("delete success");
          $('#deleteconfirm').modal('hide')
          this.goToPage(1)          

        }
        else{
          alert("data null")
        }

      },
      err=>{
        alert("Something wrong with deleteproduct")
      }
    )
  }


  
}

