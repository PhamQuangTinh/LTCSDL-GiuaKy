import {Injectable} from '@angular/core'
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router'
import {Observable} from 'rxjs'

const TOKEN_KEY = 'auth-token';
@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate{
    constructor(private router: Router){}
    canActivate(
        next: ActivatedRouteSnapshot,
        state:RouterStateSnapshot
    ):boolean{
        if(localStorage.getItem(TOKEN_KEY) != null)
            return true;
        else{
            alert("Phải đăng nhập trước khi đặt hàng")
            this.router.navigateByUrl['/trangchu/home'];
            return false;
        }
        
    }
}