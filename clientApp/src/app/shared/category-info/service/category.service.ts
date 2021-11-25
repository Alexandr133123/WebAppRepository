import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Category } from "src/app/shared/models/Category";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})

export class CategoryService {

    private url = environment.apiUrl + 'category';

    constructor(private http: HttpClient) { }

    public GetCategory(){
        return this.http.get(this.url);
    }

}