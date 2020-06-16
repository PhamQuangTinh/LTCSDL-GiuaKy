import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';

const API_URL_CATELOG = 'https://localhost:44372/api/Catelog/';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class AdminCatelogService {

    constructor(private http: HttpClient) { }

    

    pagination(page, size, keyword) : Observable<any>{
        return this.http.post(API_URL_CATELOG + "find-catelog-pagination",
        {
            page: page,
            size: size,
            keyword: keyword
        },
        httpOptions);
    }


    editCatelog(catelog) : Observable<any>{
        return this.http.post(API_URL_CATELOG + "update-catelog",
        {
            id: catelog.id,
            name: catelog.name
        }
        ,httpOptions)
    }

    createNewCatelog(catelog) : Observable<any>{
        return this.http.post(API_URL_CATELOG + "create-new-catelog",
        {
            id: catelog.id,
            name: catelog.name
        }
        ,httpOptions)
    }

    removeCatelog(catelog) : Observable<any>{
        return this.http.post(API_URL_CATELOG + "remove-catelog",
        {
            id: catelog.id,
            name: catelog.name
        }
        ,httpOptions)
    }


    
}