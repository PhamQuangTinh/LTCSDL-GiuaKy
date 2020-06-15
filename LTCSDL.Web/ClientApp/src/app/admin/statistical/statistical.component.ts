import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs'
import {AdminTransactionService} from '../admin-transaction/admin-transaction.service'

declare var $:any;

const PAGE_SIZE = 5;

@Component({
  selector: 'app-admin-statistical',
  templateUrl: './statistical.component.html',
//   styleUrls: ['./admin-transaction.component.css'],
})
export class AdminStatisticalComponent implements OnInit {

  ListTransation : []

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

  }
  
  ngOnInit(){
    this.goToPage(1);

  }

  goToPage(page){
    
    this.ObserResult = this.aTransactionService.Statistical(page,PAGE_SIZE);  
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



  findByDateTransaction(idAction){

      this.goToPage(1);
  }


  
}

