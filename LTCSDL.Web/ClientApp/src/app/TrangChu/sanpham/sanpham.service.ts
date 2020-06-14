import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = 'https://localhost:44372/api/Product/';
const API_URL_COMMENT = 'https://localhost:44372/api/Comment/';
const API_URL_BASKET = 'https://localhost:44372/api/Basket/';



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

    AddProductToBasket(proid,userid,productname,price,productInventory,productImgLink):Observable<any>{
        return this.http.post(API_URL_BASKET + 'create-product-basket',
        {
            proid: proid,
            userid: userid,
            productname: productname,
            price: price,
            productInventory: productInventory,
            productImgLink: productImgLink 
        }
        ,httpOptions);
    }

    RemoveComment(comment):Observable<any>{
        console.log(comment);
        return this.http.post(API_URL_COMMENT + 'remove-comment',
        {
            id: comment.id,
            userId: comment.userId,
            userName: comment.userName,
            productId: comment.productId,
            commentContent: comment.commentContent,
            timeComment: comment.timeComment
        }
        ,httpOptions);
    }
}