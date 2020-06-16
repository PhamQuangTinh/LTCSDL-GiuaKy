import { Component, OnInit } from '@angular/core';
import {AdminUserService} from './admin-user.service'
import {Observable} from 'rxjs'

declare var $:any;

const PAGE_SIZE = 5;

@Component({
  selector: 'app-admin-user',
  templateUrl: './admin-user.component.html',
  styleUrls: ['./admin-user.component.css'],
})
export class AdminUserComponent implements OnInit {

  ListUser : []
  
  ListRole : []

  searchName: any;

  ObserResult : Observable<any>;

  userT : any ={
        id: 0,
        username: "",
        password: "",
        roleid: 0,
        accessToken: null,
        ho: "",
        ten: "",
        email: "",
        sdt: "",
        role: null,
        transaction: []
  }
  user : any = this.userT
  totalPage: number;
  page: number;
  size: number;
  currentPage : number;
  isSuccess : boolean = false;

  isEdit : boolean;
  isDelete : boolean = false;

  constructor(
      private aUserService : AdminUserService
  ){}
  
  ngOnInit(){
    this.searchName = ""
    this.goToPage(1);
    this.getAllRole();
  }

  goToPage(page){
    this.aUserService.pagination(page,PAGE_SIZE,this.searchName).subscribe(
      res =>
      {
        if(res.success && res.data != null)
        {
          this.ListUser = res.data.data;
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

  openModal(isEdit, user){
    this.isEdit = isEdit;
    this.isDelete = false;
    if(user != null){
      this.user = user
      
    }else{
       this.user = this.userT;  
    }    
    $('#addNewUser').modal('show')
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

  getAllRole(){
    this.aUserService.getAllRole().subscribe(
      res =>{
        if(res.data != null){
          this.ListRole = res.data;
        }else{
          alert("data null")
        }
      },err=>{
        alert("Something wrong at getAllCatelog()")
      }
    )
  }

  editUser(){
    if(this.isEdit){
      this.ObserResult = this.aUserService.editUser(this.user);
    }else{
      this.ObserResult = this.aUserService.createNewUser(this.user);
    }

    this.ObserResult.subscribe(
      res =>{
        if(res.success && res.data != null){
          this.user = res.data;
          if(this.isEdit){
            alert("Sửa thành công")
          }else{
            alert("Thêm mới thành công")
            this.isEdit = true;
          }
          $('#editComfirm').modal('hide')
          $('#addNewUser').modal('hide')
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

  deleteUser(){
    this.aUserService.removeUser(this.user).subscribe(
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


  counter(i: number) {
    return new Array(i);
  }

  editComfirm(){
    $('#editComfirm').modal('show')
  }

  deleteconfirm(user){
    this.user = user;

    $('#deleteconfirm').modal('show')
  }

  

  
}

