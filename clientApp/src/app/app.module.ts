import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { DownloadModule } from './features/download-page/download.module';
import { ProductModule } from './features/product-page/product.module';
import { AppRoutingModule } from './app-routing.module';
import { ChartPageModule } from './features/chart-page/chart-page.module';
import { LoginPageModule } from './features/login-page/login-page.module';
import { AuthInterceptor } from './shared/service/auth-interceptor.service';
import { RegistrationPageModule } from './features/registration-page/registration.module';
import { LayoutModule } from '@angular/cdk/layout';

@NgModule({

  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ProductModule,
    HttpClientModule,
    DownloadModule,
    AppRoutingModule,
    ChartPageModule,
    LoginPageModule,
    RegistrationPageModule,
    LayoutModule
  ],
  declarations: [
    AppComponent
  ],
  bootstrap: [AppComponent],
  providers:[{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }]
})
export class AppModule { }
