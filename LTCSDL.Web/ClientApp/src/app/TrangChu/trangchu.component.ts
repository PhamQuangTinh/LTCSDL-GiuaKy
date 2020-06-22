import { Component,OnInit, Inject, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router,ActivatedRoute,ParamMap } from '@angular/router';
import { AuthService } from './services/auth.service';
import {TokenStorageService} from './services/token-storage.service'
import {TrangChuService} from './trangchu.service'

declare var $: any;



@Component({
  selector: 'app-trangchu',
  templateUrl: './trangchu.component.html',
  styleUrls: ['./trangchu.component.css'],

})


export class TrangChuComponent implements OnInit {

  selectedId: number;
  
  keyWordSearch : string ="";
  searchPrice1 : string ="";
  searchPrice2 : string = "";
  isLogin: boolean = false;
  alert: any = "";
  name: string;
  message: any = "";
  Roles: "";
  cateLogs: any = [];

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

  formlogout: any ={
    username: "",
    password: "",
    passwordRepeat: "",
    ho: "",
    ten: "",
    email: "",
    sdt: "",
  }

 

  constructor(
    private http: HttpClient, @Inject('BASE_URL') baseUrl: string,
    private authService: AuthService,
    private route: ActivatedRoute,
    private tokenStorage: TokenStorageService,
    private router: Router,
    private trangchuService : TrangChuService
  ) {
    this.getAllCatelog()
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
            window.location.reload()
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
    
    this.router.navigate(['/trangchu'])

    
  }

  changeProfile(){
    if(this.isLogin){
      this.router.navigate(['/trangchu/changeprofile']);
    }
    
  }

  comfirmLogout(){
    $('#logout').modal('show');
  }


  createAccount(){
    if(this.formlogout.ho != "" && this.formlogout.ten != "" && this.formlogout.email != "" && this.formlogout.sdt != ""
         && this.formlogout.password != "" && this.formlogout.passwordRepeat != "" && this.formlogout.username != "" )
    {
      var reUser = new RegExp(/^(?=[a-zA-Z0-9._]{6,20}$)(?!.*[_.]{2})[^_.].*[^_.]$/)
      var reEmail = new RegExp(/^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/);
      var rePhone = new RegExp(/^0{1}[0-9]{8}[0-9]{1}$/);

      if(!reUser.test(this.formlogout.username)){
        alert("Username không hợp lệ");
      }
      else if(!reEmail.test(this.formlogout.email)){
        alert("Email không hợp lệ");
      }
      else if(!rePhone.test(this.formlogout.sdt)){
        alert("Số điện thoại không hợp lệ")
      }
      else if(this.formlogout.password == this.formlogout.passwordRepeat){
        if(this.formlogout.password < 6) {
          alert("Mật khẩu phải có tối thiểu 6 ký tự");
          return false;
        }

        var re = new RegExp(/[0-9]/)
        if(!re.test(this.formlogout.password)) {
          alert("Mật Khẩu nên chưa ít nhất 1 số");
          return false;
        }
        var re = new RegExp(/[a-z]/)
        if(!re.test( this.formlogout.password)) {
          alert("Mật Khẩu nên chưa ít nhất 1 ký tự thường");
          return false;
        }

        var re = new RegExp(/[A-Z]/)
        if(!re.test( this.formlogout.password)) {
          alert("Mật Khẩu nên chưa ít nhất 1 ký tự in hoa");
          return false;
        }
        else{
          
          this.trangchuService.findUsername(this.formlogout.username).subscribe(
            result=>{
              if(result.success && result.data != null){
                alert("Đã tồn tại username này")
              }
              else{
                this.trangchuService.CreateUser(this.formlogout).subscribe(
                  result =>{
                      alert("success");
                      this.formlogin.username = this.formlogout.username;
                      this.formlogin.password = this.formlogout.password;
                      this.formlogout = {}
                      $('#logout').modal('hide');
                      
                      this.displayloginform();
                      
                      
                  },
                  err=>{
                    alert("Something wrong with creating new user")
                  }
                )
              }
            },
            err=>{
              alert("somethign wrong with find username")
            }
          )
          
        }

      }
      else{
        alert("Xác thực mật khẩu không chính xác, hãy kiểm tra lại");
      }
    }
    else{
      alert("Phải nhập đầy đủ thông tin")

    }
    $('#logout').modal('hide');
    
  }


  goToBasket(){
    this.router.navigate(['/trangchu/dathang',this.user.id])
  }


  getAllCatelog(){
    this.trangchuService.getAllCatelog().subscribe(
      res=>{
        this.cateLogs = res.data;
      }
      ,err=>{
        alert("something wrong")
      }
    )
  }


  getCategoryProduct(catelog){
    this.router.navigate(['/trangchu/home',catelog.id])
  }

  findProductsbyKeyWord(){
    this.router.navigate(['/trangchu/home/0',{keyword:this.keyWordSearch}])
  }


  findProductsbyPrice(){
    this.router.navigate(['/trangchu/home/0',{fPrice:this.searchPrice1, lPrice:this.searchPrice2}])
  }

  

}


