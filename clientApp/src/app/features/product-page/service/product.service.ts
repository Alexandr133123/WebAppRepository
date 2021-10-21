import {HttpClient} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable()
export class ProductService{
   
    constructor(private http: HttpClient){}

    private url = environment.apiUrl + 'product';

    GetProducts(){
        return this.http.get(this.url);
    }
    
}