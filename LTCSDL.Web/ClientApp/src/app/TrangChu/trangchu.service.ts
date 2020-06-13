import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL = 'https://localhost:44372/api/DangNhap/';
const API_CATELOG = 'https://localhost:44372/api/Catelog/';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class TrangChuService {

    constructor(private http: HttpClient) { }

    findUsername(username) : Observable<any>{
        return this.http.post(API_URL + 'find-user-by-username',
        {
            username : username
        }
        , httpOptions);
    }

    CreateUser(user) : Observable<any>{
        return this.http.post(API_URL + 'create-new-user',
        {
            id: 0,
            username : user.username,
            password : user.password,
            ho : user.ho,
            ten : user.ten,
            email : user.email,
            sdt : user.sdt
        }
        , httpOptions);
    }

    getAllCatelog() : Observable<any>{
        return this.http.post(API_CATELOG + 'get-all-catelog',httpOptions)
    }

    getCategoryProduct(categoryId): Observable<any>{
        return this.http.post(API_CATELOG + 'get-catelog-product',
        {
            categoryId : categoryId,
        }
        ,httpOptions)
    }




    


    
}