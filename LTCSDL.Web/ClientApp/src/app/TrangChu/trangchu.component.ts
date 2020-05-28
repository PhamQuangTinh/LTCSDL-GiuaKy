import { Component, Inject, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
declare var $: any;



@Component({
  selector: 'app-trangchu',
  templateUrl: './trangchu.component.html',
  styleUrls: ['./trangchu.component.css'],

})


export class TrangChuComponent {
  isLogin: boolean = false;
  alert: any = "";
  message: any = "";

  user: any = {
    id: 0,
    username: "",
    password: "",
    roleid: 0,
    ho: "",
    ten: "",
  }

  formlogin: any = {
    username: "",
    password: "",
  }

  constructor(
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string
  ) {

  }

  //Bat giao dien login
  displayloginform() {
    this.showLoginForm();
    setTimeout(function () {
      $('#loginModal').modal('show');
    }, 230);
  }

  showLoginForm() {
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

  //khi click login tren giao dien login
  login() {
    var x = this.formlogin;
    if (x.username == "") {
      this.loginAjax();
      this.resetLogin();
      this.alert = "warning";
      this.message = "Bạn chưa nhập thông tin tài khoản"
    }
    else if (x.password == "") {
      this.loginAjax();
      this.resetLogin();
      this.alert = "warning";
      this.message = "Bạn chưa nhập thông tin mật khẩu"
    }
    else {
      this.http.post('http://localhost:50809/' + 'api/DangNhap/get-by-userName', x).subscribe(
        result => {
          var res: any = result;
          if (res.data != null) {
            this.user = res.data;
            this.isLogin = true;
            $('#loginModal').modal('hide');
            this.resetLogin();
          } else {
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



}
