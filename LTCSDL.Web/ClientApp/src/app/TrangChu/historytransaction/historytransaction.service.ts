import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import{type} from './historytransaction.component'


const API_URL = 'https://localhost:44372/api/Transaction/';




const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class HistoryTransactionService {

    constructor(private http: HttpClient) { }


    findTransactionByUserAndTran(tranId,userId) : Observable<any>{
        return this.http.post(API_URL + "find-transaction-by-user-and-tran",
        {
            tranId: tranId,
            userId : userId
        },httpOptions)
    }
    
    findTransactionByUser(userId) : Observable<any>{
        return this.http.post(API_URL + "find-transaction-by-user",
        {
            userId : userId
        },httpOptions)
    }

    deleteTransaction(tranid,userid) : Observable<any>{
        return this.http.post(API_URL + "remove-transaction",
        {
            tranId: tranid,
            userId: userid,
        },httpOptions)
    }

    findTransactionByTranId(tranid) : Observable<any>{
        return this.http.post(API_URL + "find-transaction-by-transaction-id",
        {
            transactionId : tranid
        },httpOptions)
    }
    
    deleteProductOrder(tranid, proimfo:type[]) : Observable<any>{
        return this.http.post(API_URL + "remove-order-products-transaction",
        {
            id:0,
            transactionId : tranid,
            proreq : proimfo
        },httpOptions)
    }
}