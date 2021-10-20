import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TableModule } from './table/table.module';
import { HttpClientModule } from '@angular/common/http';
@NgModule({

  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    TableModule,
    HttpClientModule
  ],
  declarations: [
    AppComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
