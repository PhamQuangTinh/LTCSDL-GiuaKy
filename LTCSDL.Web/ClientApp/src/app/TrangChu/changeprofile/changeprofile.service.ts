import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


const API_URL = 'https://localhost:44372/api/DangNhap/';




const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class ChangeProfileService {

    constructor(private http: HttpClient) { }

    getUser(userId) : Observable<any>{
        return this.http.post(API_URL  + 'get-by-id',
        {      
            id : userId,
            keyword :"string"
        }, httpOptions);
    }

    updateUser(id,username,password,ho,ten,email,sdt) :  Observable<any>{
        return this.http.post(API_URL  + 'update-user-imformation',
        {      
            id : id,
            username : username,
            password : password,
            ho : ho,
            ten : ten,
            email : email,
            sdt : sdt
        }, httpOptions);
    }

}