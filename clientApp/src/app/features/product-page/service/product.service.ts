import {HttpClient, HttpParams} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Category } from "src/app/shared/models/Category";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})

export class ProductService{
   
    constructor(private http: HttpClient){}

    private url = environment.apiUrl + 'product';

    public GetProducts(){
        return this.http.get(this.url);
    }

    public GetFilteredProducts(categories: string[],productName: string,priceTo: number = 0,includeOutOfStock: boolean){
        let params = new HttpParams();
        if(!(productName.length == 0)){
            params = params.append(`productName`,productName.replace(/\s+/g, ''));
        }
        if(priceTo > 0){
            params = params.append(`priceTo`, priceTo);
        }
        if(includeOutOfStock){
            params = params.append(`includeOutOfStock`,includeOutOfStock);
        }
        categories.forEach((s: string) =>{
            params = params.append(`category`,s);
        });
        return this.http.get(this.url + '/filter',{
            params: params
        });
    }
    
}