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
        return this.http.get(API_URL + 'find-all-product', httpOptions);
    }

    
}