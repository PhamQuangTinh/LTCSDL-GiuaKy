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


  
export class DatHangService {
    products: Array<any> = [];

    constructor(private http: HttpClient) { }

    findProductByID(proID) : Observable<any>{
        return this.http.post(API_URL + 'find-product-by-id',
        {
            productId : proID,
        }, httpOptions);
    }

    saveProducts(product){
        this.products.push(product);
    }

    getProductsAfterOrder(product){
        this.saveProducts(product);
        return this.products
    }
    
    getProductsBeforOrder(){
        return this.products
    }

    


    
}