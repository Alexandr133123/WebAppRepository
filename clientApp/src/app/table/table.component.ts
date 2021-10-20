import { Component, OnInit} from '@angular/core';
import { HttpRequestService } from '../service/http.service';
import { Product } from '../models/Product';
@Component({
  selector: 'table-comp',
  templateUrl: './table.component.html',
  providers: [HttpRequestService]
})

export class TableComponent  implements OnInit{

  displayedColumns = ['Id','Name','Price'];
  products: Product[];
  constructor(private httpservice: HttpRequestService){}

  ngOnInit(){
    this.loadProduct();
  }

  loadProduct(){
    this.httpservice.HttpGet().subscribe((data: Product[]) => this.products = data);
  }

}
