import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = 'https://localhost:44372/api/Product/';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class ProductService {

    constructor(private http: HttpClient) { }

    OrderDetail(productId): Observable<any> {
    
        return this.http.post(API_URL+ 'find-product-by-id',
        {
            id: productId,
        }
        , httpOptions);
    }
}