import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {ChangeProfileService} from './changeprofile.service'
import {TokenStorageService} from '../services/token-storage.service'
declare var $:any;
@Component({
  selector: 'app-changeprofile',
  templateUrl: './changeprofile.component.html',
  styleUrls: ['./changeprofile.component.css']
})
export class ChangeProfileComponent implements OnInit{
    userId: number;

    user : any = {
        
    }
    UserAfterChangeProfile : any;
    mapUser: any = {
        ho: "",
        ten: "",
        email: "",
        sdt: "",
        oldPassword: "",
        newPassword: "",
        confirmPassword: "",
    }
    ngOnInit(){
        this.mapUser.ho = this.user.ho;
        this.mapUser.ten = this.user.ten;
        this.mapUser.sdt = this.user.sdt;
        this.mapUser.email = this.user.email;
        this.UserAfterChangeProfile = this.user;
    }
    constructor
    (
        private router: Router,
        private route: ActivatedRoute,
        private changeprofile:ChangeProfileService,
        private token : TokenStorageService
    )
    {
        this.user = this.token.getUser();
    }

    getUserProfile(){
        this.changeprofile.getUser(this.userId).subscribe(
            res =>{
                if(res.success && res.data != null){
                    this.user = res.data;
                    console.log(this.user)  
                }else{
                    alert("No exist user")
                }
            },
            err=>{
                alert("something wrong")
            }
        )
    }

    changeProfile(){
        $('#changeProfile').modal('hide')
        if(this.mapUser.ho != "" && this.mapUser.ten != "" && this.mapUser.email != "" && this.mapUser.sdt != ""
        && this.mapUser.oldPassword != "" && this.mapUser.newPassword != "" && this.mapUser.confirmPassword != "")
        {
            if(this.mapUser.oldPassword === this.user.password){
                if(this.mapUser.newPassword != "" && this.mapUser.newPassword == this.mapUser.confirmPassword) {
                    if(this.mapUser.newPassword.length < 6) {
                      alert("Mật khẩu phải có tối thiểu 6 ký tự");
                      return false;
                    }

                    var re = new RegExp(/[0-9]/)
                    if(!re.test(this.mapUser.newPassword)) {
                      alert("Mật Khẩu nên chưa ít nhất 1 số");
                      return false;
                    }
                    var re = new RegExp(/[a-z]/)
                    if(!re.test( this.mapUser.newPassword)) {
                      alert("Mật Khẩu nên chưa ít nhất 1 ký tự thường");
                      return false;
                    }

                    var re = new RegExp(/[A-Z]/)
                    if(!re.test( this.mapUser.newPassword)) {
                      alert("Mật Khẩu nên chưa ít nhất 1 ký tự in hoa");
                      return false;
                    }

                    var re = new RegExp(/^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/)
                    if(!re.test(this.mapUser.email)){
                        alert("email khong hop le")
                    }
                    var re = new RegExp(/^0{1}[0-9]{8}[0-9]{1}$/)
                    if(!re.test(this.mapUser.sdt)){
                        alert("Số điện thoại không hợp lệ")
                    }

                    else{
                        
                        
                        this.changeprofile.updateUser(this.user.id,this.user.username,this.mapUser.newPassword,
                            this.mapUser.ho,this.mapUser.ten,this.mapUser.email,this.mapUser.sdt).subscribe(
                                res=>{

                                        alert("change success");
                                        this.UserAfterChangeProfile.ho = this.mapUser.ho;
                                        this.UserAfterChangeProfile.ten = this.mapUser.ten;
                                        this.UserAfterChangeProfile.email = this.mapUser.email
                                        this.UserAfterChangeProfile.newPassword = this.mapUser.newPassword
                                        this.UserAfterChangeProfile.sdt = this.mapUser.sdt
                                        this.token.saveUser(this.UserAfterChangeProfile)
                                        this.router.navigate(['/trangchu/home'])
                                    
                                },
                                err=>
                                {
                                    alert("something wrong")

                                }
                            )
                        

                    }

                  } else {
                    alert("Xác thực mật khẩu không chính xác, hãy kiểm tra lại");
                    return false;
                  }
                
            }
            else{
                alert("Mật khẩu cũ không chính xác");
            }
        }
        else{
            alert("Phải nhập đầy đủ thông tin")
        }
        
    }


    saveChanges()
    {
        $('#changeProfile').modal('show')
    }





   



    
}
