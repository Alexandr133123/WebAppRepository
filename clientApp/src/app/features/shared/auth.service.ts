import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})

export class AuthorizationiService{
    private url = environment.apiUrl + 'authorization';

    constructor(private http: HttpClient){}

    public authorize(login: string, password: string){
        const body = {
            username: login,
            password: password
        }
        return this.http.post(this.url + '/' + 'login', body, {withCredentials: true});
    }
    public register(login: string, password: string){
        const body = {
            username: login,
            password: password
        }
        return this.http.post(this.url + '/' + 'register', body, {withCredentials: true});
    }

}