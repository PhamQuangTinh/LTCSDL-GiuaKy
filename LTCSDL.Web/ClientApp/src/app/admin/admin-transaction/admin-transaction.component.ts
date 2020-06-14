import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs'
import {AdminTransactionService} from './admin-transaction.service'

declare var $:any;

const PAGE_SIZE = 5;

@Component({
  selector: 'app-admin-transaction',
  templateUrl: './admin-transaction.component.html',
//   styleUrls: ['./admin-transaction.component.css'],
})
export class AdminTransactionComponent implements OnInit {

  ListTransation : []

  idAction : number;
  ObserResult : Observable<any>;
  isDate : Observable<any>;
  transaction: any;
  totalPage: number;
  page: number;
  size: number;
  currentPage : number;
  isSuccess : boolean = false;

  dateTransaction: Date = new Date();
  isEdit : boolean;
  isDelete : boolean = false;

  constructor(
    private aTransactionService :AdminTransactionService,
  ){
      this.idAction = 0;
  }
  
  ngOnInit(){
    this.goToPage(1);

  }

  goToPage(page){
    
    if(this.idAction == 0){
        this.ObserResult = this.aTransactionService.pagination(page,PAGE_SIZE,0,this.dateTransaction);
    }else if(this.idAction == 1){
        this.ObserResult = this.aTransactionService.pagination(page,PAGE_SIZE,1,this.dateTransaction);
    }
    
    this.ObserResult.subscribe(
      res =>
      {
        if(res.success && res.data != null)
        {
          this.ListTransation = res.data.data;
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


  deleteconfirm(transaction){
    this.transaction = transaction;

    $('#deleteconfirm').modal('show')
  }


    deleteTransaction(){
        this.aTransactionService.removeTransaction(this.transaction).subscribe(
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

  findByDateTransaction(idAction){
      this.idAction = idAction;
      this.goToPage(1);
  }


  
}

