import { Component, OnInit} from '@angular/core';
import { ProductService } from './service/product.service';
import { Product } from '../../shared/models/Product';

@Component({
  selector: 'product-comp',
  templateUrl: './product.component.html',
})

export class ProductComponent  implements OnInit{

  displayedColumns = ['Id','Name','Price'];
  products: Product[];
  
  constructor(private httpservice: ProductService){}

  ngOnInit(){
    this.loadProducts();
  }

  loadProducts(){
    this.httpservice.GetProducts().subscribe((data: Product[]) => this.products = data);
  }

}
