import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';

const API_URL_PRODUCT = 'https://localhost:44372/api/Product/';
const API_URL_CATELOG = 'https://localhost:44372/api/Catelog/';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

@Injectable({
    providedIn: 'root'
})


  
export class AdminProductService {

    constructor(private http: HttpClient) { }

    findAllProduct() : Observable<any>{
        return this.http.post(API_URL_PRODUCT + 'find-all-product', httpOptions);
    }

    pagination(page, size ,id ,categoryId,keyword) : Observable<any>{
        return this.http.post(API_URL_PRODUCT + "search-product",
        {
            page: page,
            size: size,
            id: id,
            categoryId : parseInt(categoryId),
            type: "string",
            keyword: keyword
        },
        httpOptions);
    }

    getAllCatelog(): Observable<any>{
        return this.http.post(API_URL_CATELOG + "get-all-catelog",httpOptions);
    }

    editProduct(product) : Observable<any>{
        return this.http.post(API_URL_PRODUCT + "edit-product",
        {
            id: product.id,
            catelogId: parseInt(product.catelogId),
            productname: product.productname,
            price: product.price,
            description: product.description,
            productcontent: product.productcontent,
            productInventory: product.productInventory,
            productImgLink: product.productImgLink
        }
        ,httpOptions)
    }

    createNewProduct(product) : Observable<any>{
        return this.http.post(API_URL_PRODUCT + "create-new-product",
        {
            id: product.id,
            catelogId: parseInt(product.catelogId),
            productname: product.productname,
            price: product.price,
            description: product.description,
            productcontent: product.productcontent,
            productInventory: product.productInventory,
            productImgLink: product.productImgLink
        }
        ,httpOptions)
    }

    removeProduct(product) : Observable<any>{
        return this.http.post(API_URL_PRODUCT + "remove-product-by-model",
        {
            id: product.id,
            catelogId: parseInt(product.catelogId),
            productname: product.productname,
            price: product.price,
            description: product.description,
            productcontent: product.productcontent,
            productInventory: product.productInventory,
            productImgLink: product.productImgLink
        }
        ,httpOptions)
    }


    
}