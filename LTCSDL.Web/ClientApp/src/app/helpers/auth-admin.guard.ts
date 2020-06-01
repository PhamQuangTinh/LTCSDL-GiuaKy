import {Injectable} from '@angular/core'
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router'
import { TokenStorageService } from '../services/token-storage.service';

import {Observable} from 'rxjs'


const ROLE_KEY = 'role-user';
@Injectable({
    providedIn: 'root'
})
export class AuthAdminGuard implements CanActivate{

    
    constructor(
        private router: Router,
        private token: TokenStorageService,
    ){}
    canActivate(
        next: ActivatedRouteSnapshot,
        state:RouterStateSnapshot
    ):boolean{
        const tokenRequest = this.token.getRole();
        console.log(tokenRequest);
         if(tokenRequest === "ADMIN"){
            return true;
        }
            
        else{
            alert("Không đủ quyền")
            this.router.navigate['/trangchu/home'];
            return false;
        }
        
    }
}