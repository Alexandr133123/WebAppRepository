import { Component } from "@angular/core";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { first } from "rxjs/operators";
import { AuthorizationiService } from "../shared/auth.service";

@Component({
    selector: 'registration-comp',
    templateUrl: './registration-page.component.html',
    styleUrls: ['registration.page.component.scss']
})

export class RegistrationPageComponent{
    isLinear = true;
    authorizationFormControl: FormControl;
    loginForm: FormGroup;
    isDisplayable: boolean;
    isAuthorizationSuccess: boolean;
    passwordForm: FormGroup;
    constructor(private _formBuilder: FormBuilder, private registerService: AuthorizationiService ) {}
  
    ngOnInit() {
        this.loginForm = new FormGroup({
            "login": new FormControl('', Validators.required),            
        });
        this.passwordForm = new FormGroup({
            'password': new FormControl('', Validators.required)
        });
    }
    public register(){
        const login = this.loginForm.controls['login'].value;
        const password = this.passwordForm.controls['password'].value; 
        this.registerService.register(login, password).pipe(first()).subscribe(
            data => {this.isDisplayable = true;  this.isAuthorizationSuccess = true},
            error => {this.isDisplayable = true; this.isAuthorizationSuccess = false});
    }
}