import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { type } from './dathang.component';

const API_URL = 'https://localhost:44372/api/Product/';
const API_URL_BASKET = 'https://localhost:44372/api/Basket/';
const API_URL_TRANSACTION = 'https://localhost:44372/api/Transaction/'



const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class DatHangService {

    constructor(private http: HttpClient) { }

    findBasketByUserId(userId) : Observable<any>{
        return this.http.post(API_URL_BASKET  + 'get-basket',
        {
            userId : userId,
        }, httpOptions);
    }

    deleteProductInBasket(userId,proId) : Observable<any>{
        return this.http.post(API_URL_BASKET  + 'delete-product-basket',
        {
            userId : userId,
            proId : proId
        }, httpOptions);
    }
    deleteBasket(userId): Observable<any>{
        return this.http.post(API_URL_BASKET + 'delete-basket',
        {
            userId : userId
        },httpOptions)
    }

    createNewTransaction(userId , amount, proimfo:type[]) : Observable<any>{
        return this.http.post(API_URL_TRANSACTION  + 'create-new-transaction-with-many-products',
        {
            id: 0,
            userId: userId,
            amount: amount,
            proreq : proimfo
            
  
        }, httpOptions);
    }

    updateBasket(userId,proId,proIvent) : Observable<any>{
        return this.http.post(API_URL_BASKET + 'update-basket',
        {
            userId: userId,
            proId: proId,
            proIvent : proIvent
            
  
        }, httpOptions);
    }

    

    

    


    
}