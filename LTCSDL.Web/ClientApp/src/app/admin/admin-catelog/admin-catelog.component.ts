import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs'
import {AdminCatelogService} from './admin-catelog.service'

declare var $:any;

const PAGE_SIZE = 5;

@Component({
  selector: 'app-admin-catelog',
  templateUrl: './admin-catelog.component.html',
  styleUrls: ['./admin-catelog.component.css'],
})
export class AdminCatelogComponent implements OnInit {

  ListCatelog : []

  catelogName : any;

  ObserResult : Observable<any>;

  catelogT : any ={
    id: 0,
    name : "",
  }
  catelog : any = this.catelogT
  totalPage: number;
  page: number;
  size: number;
  currentPage : number;
  isSuccess : boolean = false;

  isEdit : boolean;
  isDelete : boolean = false;

  constructor(
    private aProductService :AdminCatelogService,
  ){}
  
  ngOnInit(){
    this.catelogName = "";
    this.goToPage(1);
  }

  goToPage(page){
    this.aProductService.pagination(page,PAGE_SIZE,this.catelogName).subscribe(
      res =>
      {
        if(res.success && res.data != null)
        {
          this.ListCatelog = res.data.data;
          this.totalPage = res.data.totalPage;
          this.page = res.data.page;
          this.size = res.data.size;
          this.currentPage = page;
          this.isSuccess = true;
          
          console.log(this.ListCatelog)
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

  openModal(isEdit, catelog){
    this.isEdit = isEdit;
    this.isDelete = false;
    if(catelog != null){
      this.catelog = catelog
      
    }else{
       this.catelog = this.catelogT;  
    }    
    $('#addNewCatelog').modal('show')
  }

  editComfirm(){
    $('#editComfirm').modal('show')
  }
  deleteconfirm(catelog){
    this.catelog = catelog;

    $('#deleteconfirm').modal('show')
  }

  

  editProduct(){
    if(this.isEdit){
      this.ObserResult = this.aProductService.editCatelog(this.catelog);
    }else{
      this.ObserResult = this.aProductService.createNewCatelog(this.catelog);
    }

    this.ObserResult.subscribe(
      res =>{
        if(res.success && res.data != null){
          this.catelog = res.data;
          if(this.isEdit){
            $('#addNewCatelog').modal('hide')
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
    console.log(this.catelog)
    this.aProductService.removeCatelog(this.catelog).subscribe(
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

