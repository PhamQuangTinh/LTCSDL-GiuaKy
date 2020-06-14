import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_URL_USER = 'https://localhost:44372/api/DangNhap/';
const API_URL_ROLE = 'https://localhost:44372/api/Role/';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class AdminUserService {

    constructor(private http: HttpClient) { }

    

    pagination(page, size) : Observable<any>{
        return this.http.post(API_URL_USER + "search-user-pagination",
        {
            page: page,
            size: size,
            keyword: ""
        },
        httpOptions);
    }

    getAllRole(): Observable<any>{
        return this.http.post(API_URL_ROLE + "get-all-role",httpOptions);
    }

    editUser(user) : Observable<any>{
        return this.http.post(API_URL_USER + "update-user-imformation",
        {
            id: user.id,
            roleid: parseInt(user.roleid),
            username: user.username,
            password: user.password,
            ho: user.ho,
            ten: user.ten,
            email: user.email,
            sdt: user.sdt
        }
        ,httpOptions)
    }

    createNewUser(user) : Observable<any>{
        return this.http.post(API_URL_USER + "create-new-user",
        {
            id: user.id,
            roleid: parseInt(user.roleid),
            username: user.username,
            password: user.password,
            ho: user.ho,
            ten: user.ten,
            email: user.email,
            sdt: user.sdt
        }
        ,httpOptions)
    }

    removeUser(user) : Observable<any>{
        console.log(user)
        return this.http.post(API_URL_USER + "remove-user",
        {
            id: user.id,
            roleid: user.roleid,
            username: user.username,
            password: user.password,
            ho: user.ho,
            ten: user.ten,
            email: user.email,
            sdt: user.sdt
        }
        ,httpOptions)
    }


    
}