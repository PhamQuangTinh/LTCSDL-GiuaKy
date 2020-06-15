import { Component, OnInit } from '@angular/core';
import {TokenStorageService} from '/Users/pc/Desktop/LTCSDL-GiuaKy/LTCSDL.Web/ClientApp/src/app/services/token-storage.service'
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import {HistoryTransactionService} from './historytransaction.service';
declare var $:any

@Component({
  selector: 'app-history',
  templateUrl: './historytransaction.component.html',
  styleUrls: ['./historytransaction.component.css']
})
export class HistoryTransactionComponent implements OnInit {
    isEdit : any;
    user: any
    listTransaction : any = []
    OrdersTran : any = []
    ProIdvsPronum: type[] = []

    constructor(
        private historyTranService : HistoryTransactionService,
        private router: Router,
        private route: ActivatedRoute,
        private token : TokenStorageService
    )
    {
        this.user = this.token.getUser();
    }

    ngOnInit(){
        this.findTransactionByUserId(this.user.id)
        
        
        

    }
    findTransactionByUserId(userId)
    {
        this.historyTranService.findTransactionByUser(userId).subscribe(
            res =>{
                if(res.success && res.data != null){
                    this.listTransaction = res.data;
                }
                else{
                    alert("Data null")
                        $('#editTranSaction').modal('hide')
                        this.findTransactionByUserId(this.user.id)
                }
            },
            err =>{
                alert("Something wrong")
            }
        )
    }




    deleteTranSaction(transaction){
        this.historyTranService.deleteTransaction(transaction.id,transaction.userId).subscribe(
            res=>{
                if(res.success && res.data != null){
                    alert("Delete Success");
                    $('#deleteTranSaction').modal('hide')
                    this.findTransactionByUserId(this.user.id)
                }
                else{
                    alert("data null")
                }
            },
            err =>{
                alert("Something wrong")
            }
        )
    }

    deleteComfirmation(){
        this.isEdit = "delete"
        $('#deleteTranSaction').modal('show')
    }

    editComfirmation(tran){
        this.historyTranService.findTransactionByTranId(tran.id).subscribe(
            res =>{
                if(res.success && res.data != null){
                    this.OrdersTran = res.data;
                    this.findTransactionByUserId(this.user.id)   
                    
                }
                else{
                    alert("data null")
                }
            },
            err =>{

                alert("something wrong")
            }
        )


        $('#editTranSaction').modal('show')
    }

    deleteProductComfirmation(){
        $('#deleteProdcutTransaction').modal('show')
    }
    closeDeleteProduct(){        
        $('#deleteProdcutTransaction').modal('hide')

        
    }

    deleteProduct(product){
        this.ProIdvsPronum.push({proId : product.productId,proNum : product.quantity})
        this.historyTranService.deleteProductOrder(product.id,this.ProIdvsPronum).subscribe(
            res =>{
                if(res.success && res.data != null)
                {
                    alert("delete success")

                    $('#deleteProdcutTransaction').modal('hide')
                    this.editComfirmation(product);
                }else{
                    alert("data null")
                }
            },
            err=>{
                alert("something wrong")
            }
        )
    }
    
    checkDate(datetime) : boolean{
        let currentDate = new Date();
        let date = new Date(datetime)
        if((currentDate.getFullYear() == date.getFullYear()) && 
            (currentDate.getMonth() == date.getMonth()) && (currentDate.getDate() == date.getDate()))
        {
            return true;
        }
        return false;
    }
}

export interface type{
    proId: number;
    proNum: number;
  }
