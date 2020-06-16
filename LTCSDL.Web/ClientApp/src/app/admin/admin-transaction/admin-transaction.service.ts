import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL_TRANSACTION = 'https://localhost:44372/api/Transaction/';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class AdminTransactionService {

    constructor(private http: HttpClient) { }

    

    pagination(page, size, id,datetime1,datetime2) : Observable<any>{
        return this.http.post(API_URL_TRANSACTION + "search-transaction-pagination",
        {
            id : id,
            page: page,
            size: size,
            dateTime1: datetime1,
            dateTime2: datetime2,
        },
        httpOptions);
    }


    editCatelog(catelog) : Observable<any>{
        return this.http.post(API_URL_TRANSACTION + "update-catelog",
        {
            id: catelog.id,
            name: catelog.name
        }
        ,httpOptions)
    }

    createNewCatelog(catelog) : Observable<any>{
        return this.http.post(API_URL_TRANSACTION + "create-new-catelog",
        {
            id: catelog.id,
            name: catelog.name
        }
        ,httpOptions)
    }

    removeTransaction(transaction) : Observable<any>{
        return this.http.post(API_URL_TRANSACTION + "remove-transaction",
        {
            tranId: transaction.id,
            userId: transaction.userId
        }
        ,httpOptions)
    }

    findByDateTransactino(date) : Observable<any>{
        return this.http.post(API_URL_TRANSACTION + "remove-transaction",
        {
            tranId: date,
        }
        ,httpOptions)
    }

    Statistical(page,size,datetime1,datetime2) : Observable<any>{
        console.log(page,size,datetime1,datetime2)
        return this.http.post(API_URL_TRANSACTION + "Statistical-by-date",
        {
            page: page,
            size : size,
            dateTime1 : datetime1,
            dateTime2 : datetime2
        }
        ,httpOptions)
    }


    
}