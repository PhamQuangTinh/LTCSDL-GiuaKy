import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = 'https://localhost:44372/api/Product/';
const API_URL_COMMENT = 'https://localhost:44372/api/Comment/';
const API_URL_TRANSACTION = 'https://localhost:44372/api/Transaction/';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class ProductService {

    constructor(private http: HttpClient) { }

    FindProductById(proId : number): Observable<any> {
        
        return this.http.post(API_URL+ 'find-product-by-id',
        {
            productId: proId,
        }
        , httpOptions);
    }

    GetCommentProduct(proId: number) : Observable<any>{
        return this.http.post(API_URL_COMMENT + 'get-comment-product',
        {
            productId: proId,
        }
        , httpOptions);
    }

    CreateNewComment(UserId:number, productId: number, content: any): Observable<any>{
        return this.http.post(API_URL_COMMENT + 'create-comment',
        {
            userId: UserId,
            productId: productId,
            commentContent:content 
        }
        ,httpOptions);
    }

    CreateNewTransaction(userId:number, amount: any,productId:number,productnum:number):Observable<any>{
        return this.http.post(API_URL_TRANSACTION + 'create-new-transaction-with-many-products',
        {
            id:0,
            userId: userId,
            amount: amount,
            proreq: [
                {
                    proId : productId,
                    proNum : productnum,
                }
            ]
        }
        ,httpOptions);
    }
}