import {HttpClient} from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class HttpRequestService{
   
    constructor(private http: HttpClient){}

    private url = 'http://localhost:12564/api/product';

    HttpGet(){
        return this.http.get(this.url);
    }
    
}