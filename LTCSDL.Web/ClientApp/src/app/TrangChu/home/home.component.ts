import { Component } from '@angular/core';
import {  ActivatedRoute, RouteConfigLoadEnd } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  id:string;

  constructor( ){
    // activeRoute.params.subscribe(params => {this.id = params['id']});
  }
  
}
