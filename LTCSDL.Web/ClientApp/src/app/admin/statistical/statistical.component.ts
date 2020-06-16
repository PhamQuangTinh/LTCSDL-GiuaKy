import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs'
import {AdminTransactionService} from '../admin-transaction/admin-transaction.service'

declare var $:any;

const PAGE_SIZE = 5;

@Component({
  selector: 'app-admin-statistical',
  templateUrl: './statistical.component.html',
})
export class AdminStatisticalComponent implements OnInit {

  ListTransation : []

  date1: Date = new Date("2000-01-01T00:00:00");
  date2: Date = new Date();

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
    
    this.ObserResult = this.aTransactionService.Statistical(page,PAGE_SIZE,this.date1,this.date2);  
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

          
          console.log(this.ListTransation)
          
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



  
}

