import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { HomeModule } from './features/home-page/home.module';
import { ProductModule } from './features/product-page/product.module';
import { AppRoutingModule } from './app-routing.module';

@NgModule({

  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ProductModule,
    HttpClientModule,
    HomeModule,
    AppRoutingModule,
  ],
  declarations: [
    AppComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
