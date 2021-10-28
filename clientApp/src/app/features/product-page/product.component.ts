import { AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import { ProductService } from './service/product.service';
import { Product } from '../../shared/models/Product';
import { FilterEventService } from './service/filter-event.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
@Component({
  selector: 'product-comp',
  templateUrl: './product.component.html',
})

export class ProductComponent  implements OnInit, AfterViewInit{
  constructor(private productService: ProductService, private eventService: FilterEventService){}
  private productNameFilter: string;
  private productCategoriesFilter: string[]
  private productPriceFilter:number;
  private productIncludeOutOfStockFilter: boolean;
  public maxProductPrice: number;
  public displayedColumns = ['card'];
  public dataSource = new MatTableDataSource<Product>();
  @ViewChild('paginator') paginator: MatPaginator;

  public ngOnInit(){
    this.loadProducts();
    
  }
  public ngAfterViewInit(){
    this.dataSource.paginator = this.paginator;
  }

  public getNameFilter(nameInputString: string){
    this.productNameFilter=nameInputString;
  }
  public getIncludeOutOfStock(includeOutOfStock: boolean){
    this.productIncludeOutOfStockFilter = includeOutOfStock;
  }
  public getPriceFilter(productPriceFilter: number){
    this.productPriceFilter = productPriceFilter;
  }
  public getCategoriesFilter(categories: string[]){
    this.productCategoriesFilter=categories;
    this.startSearch();
  }
  
  private startSearch(){
      this.productService.GetFilteredProducts(this.productCategoriesFilter,this.productNameFilter,this.productPriceFilter,this.productIncludeOutOfStockFilter).subscribe((data: Product[]) => this.dataSource.data = data);
  }

  public loadProducts() {
    this.productService.GetProducts().subscribe((data: Product[]) => this.dataSource.data = data).add(() => this.maxProductPrice = Math.max(...this.dataSource.data.map(p => p.price)));
  }
  public onDeleteAction(productId: number){
      alert(productId);
  }
}
