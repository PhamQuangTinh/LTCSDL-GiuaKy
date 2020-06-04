import { Component } from '@angular/core';
import {  ActivatedRoute,Router,ParamMap } from '@angular/router';
import { HomeService} from './home.service'


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  // departments = [
  //   {"id": 1, "name": "Angular"},
  //   {"id": 2, "name": "Node"},
  //   {"id": 3, "name": "MongoDB"},
  //   {"id": 4, "name": "Bootstrap"},
  // ]
  ListProduct: []
  product: any = {
      id: 0,
      catelogId: 0,
      productname: "",
      price: "",
      description: "",
      productcontent: "",
      productInventory: 0,
      productImgLink: "",
      catelog: null,
      order: []
  }
  constructor( 
    private router: Router, private route: ActivatedRoute,
    private homeservice: HomeService
    ){
    // this.route.paramMap.subscribe((params : ParamMap)=>
    // {
    //   let id = parseInt(params.get('id'));
    //   this.productId = id;
    // });
    this.getAllproduct();
  }

  getAllproduct(){
    this.homeservice.findAllProduct().subscribe(
      result => {
        if(result != null){
          console.log(result.data);
          this.ListProduct = result.data;
        }
        else{
          alert('Nothing to show()');
        }
      }
    )
  }
  onSelect(department){
    this.router.navigate(['/trangchu/thongtinsanpham',department.id]);
  }
  
}
