import {HttpClient} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Category } from "src/app/shared/models/Category";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})

export class CategoryService{
   
    constructor(private http: HttpClient){}

    private url = environment.apiUrl + 'category';

    GetCategory(): Observable<Category[]>{
        return this.http.get<Category[]>(this.url);
    }
    
}