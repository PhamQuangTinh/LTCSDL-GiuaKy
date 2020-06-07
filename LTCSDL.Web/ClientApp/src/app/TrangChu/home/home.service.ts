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


  
export class HomeService {

    constructor(private http: HttpClient) { }

    findAllProduct() : Observable<any>{
        return this.http.post(API_URL + 'find-all-product', httpOptions);
    }

    pagination(page, size) : Observable<any>{
        return this.http.post(API_URL + "search-product",
        {
            page: page,
            size: size,
            id: 0,
            type: "string",
            keyword: ""
        },
        httpOptions);
    }


    
}