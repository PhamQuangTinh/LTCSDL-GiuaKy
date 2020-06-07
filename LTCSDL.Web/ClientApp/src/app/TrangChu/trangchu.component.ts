import { Component,OnInit, Inject, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router,ActivatedRoute,ParamMap } from '@angular/router';
import { AuthService } from '../services/auth.service';
import {TokenStorageService} from '../services/token-storage.service'

declare var $: any;



@Component({
  selector: 'app-trangchu',
  templateUrl: './trangchu.component.html',
  styleUrls: ['./trangchu.component.css'],

})


export class TrangChuComponent implements OnInit {

  selectedId: number;
  
  isLogin: boolean = false;
  alert: any = "";
  name: string;
  message: any = "";
  Roles: "";

  user: any = {
    id: 0,
    username: "",
    password: "",
    roleid: 0,
    accessToken: "",
    refreshToken: "",
    ho: "",
    ten: "",
    email: "",
    sdt: "",
    role: [],
    transaction: []
  }

  formlogin: any = {
    username: "",
    password: "",
  }

 

  constructor(
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
    private authService: AuthService,
    private route: ActivatedRoute,
    private tokenStorage: TokenStorageService,
    private router: Router
  ) {

  }
  
  ngOnInit() {
    if (this.tokenStorage.getToken()) {
      this.isLogin = true;
      this.user = this.tokenStorage.getUser();

      this.Roles = this.tokenStorage.getUser().role.code;  
    }
    
  }


  //Bat giao dien login
  displayloginform() {
    this.showLoginForm();
    setTimeout(function () {
      $('#loginModal').modal('show');
    }, 230);
  }

  showLoginForm() {
    this.alert = "";
    this.message = "";
    $('#loginModal .registerBox').fadeOut('fast', function () {
      $('.loginBox').fadeIn('fast');
      $('.register-footer').fadeOut('fast', function () {
        $('.login-footer').fadeIn('fast');
      });

      $('.modal-title').html('Login with');
    });
    // $('.error').removeClass('alert alert-danger').html('');
  }


  showRegisterForm() {
    this.alert = ""
    this.message = ""
    $('.loginBox').fadeOut('fast', function () {
      $('.registerBox').fadeIn('fast');
      $('.login-footer').fadeOut('fast', function () {
        $('.register-footer').fadeIn('fast');
      });
      $('.modal-title').html('Register with');
    });
    // $('.error').removeClass('alert alert-danger').html('');

  }

  
  loginAjax() {
    this.shakeModal();

  }

  shakeModal() {
    $('#loginModal .modal-dialog').addClass('shake');
    $('.error').addClass('alert alert-danger').html("Invalid email/password combination");
    $('input[type="password"]').val('');
    setTimeout(function () {
      $('#loginModal .modal-dialog').removeClass('shake');
    }, 1000);
  }
  displayreigisterform() {
    this.showRegisterForm();
    setTimeout(function () {
      $('#loginModal').modal('show');
    }, 230);
  }

  //Reset username pass word;
  resetLogin() {
    this.formlogin.username = "";
    this.formlogin.password = "";
  }

  reloadPage() {
    window.location.reload();
  }

  //khi click login tren giao dien login
  login() {
    var x = this.formlogin;
    if (x.username == "") {
      
      this.isLogin = false;
      this.loginAjax();
      this.resetLogin();
      this.alert = "warning";
      this.message = "Bạn chưa nhập thông tin tài khoản"
    }
    else if (x.password == "") {
      this.isLogin = false;
      this.loginAjax();
      this.resetLogin();
      this.alert = "warning";
      this.message = "Bạn chưa nhập thông tin mật khẩu"
    }
    else {
      this.authService.login(x).subscribe(
        result => {
          var res: any = result;
          //Login success
          if (res.data != null) {
            this.isLogin = true;
            this.user = res.data; 
            this.tokenStorage.saveUser(res.data)
            //Lưu token
            this.tokenStorage.saveToken(res.data.accessToken);
            this.tokenStorage.saveRole(res.data.role.code);
            $('#loginModal').modal('hide');
            this.resetLogin();
          } else {
            this.isLogin = false;
            this.loginAjax();
            this.alert = "danger";
            this.message = "Tài Khoản hoặc mật khẩu không đúng"
            
            this.resetLogin();
          }
        }, error => {
          console.error(error);
          alert(error);
        }
      );
    }
    

  }
  logout(){
    this.tokenStorage.signOut();
    this.isLogin = false;
    this.router.navigate(['']);
  }




}
