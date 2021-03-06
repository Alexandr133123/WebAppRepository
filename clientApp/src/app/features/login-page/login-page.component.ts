import { STEPPER_GLOBAL_OPTIONS } from "@angular/cdk/stepper";
import {Component, OnInit} from "@angular/core"
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { first } from "rxjs/operators";
import { AuthEventService } from "src/app/shared/service/authorization-event.service";
import { AuthorizationiService } from "../shared/auth.service";


@Component({
    selector: 'login-comp',
    templateUrl: 'login-page.component.html',
    styleUrls: ['login-page.component.scss'],
    providers: [
        {
          provide: STEPPER_GLOBAL_OPTIONS,
          useValue: {showError: true},
        },
      ]
})

export class LoginPageComponent implements OnInit{
    isLinear = true;
    isDisplayable: boolean;
    isAuthorizationSuccess: boolean;
    loginForm: FormGroup;
    passwordForm: FormGroup;
    constructor(private _formBuilder: FormBuilder, private loginService: AuthorizationiService, private authEvent: AuthEventService) {}
  
    ngOnInit() {
        this.loginForm = new FormGroup({
            "login": new FormControl('', Validators.required),            
        });
        this.passwordForm = new FormGroup({
            'password': new FormControl('', Validators.required)
        });
    }

    public authorize(){
        const login = this.loginForm.controls['login'].value;
        const password = this.passwordForm.controls['password'].value; 
        this.loginService.authorize(login, password).pipe(first())
            .subscribe(
                data => {this.isDisplayable = true;  this.isAuthorizationSuccess = true},
                error => {this.isDisplayable = true; this.isAuthorizationSuccess = false},
                () => this.authEvent.atAuthorized.next());
    }
        
    

}