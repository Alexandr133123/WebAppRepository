import { AfterViewInit, Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ProductService } from './service/product.service';
import { Product } from '../../shared/models/Product';
import { FilterEventService } from './service/filter-event.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { ProductResponse } from 'src/app/shared/models/ProductResponse';
import { MatDialog } from '@angular/material/dialog';
import { AddProductComponent } from './features/add-product/add-product.component';
import { Category } from 'src/app/shared/models/Category';
@Component({
  selector: 'product-comp',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})

export class ProductComponent implements OnInit, AfterViewInit, OnDestroy {
  private productNameFilter: string;
  private product: Product;
  private subscribtionInfo: Subscription;
  private productCategoriesFilter: string[]
  private productPriceFilter: number;
  private productIncludeOutOfStockFilter: boolean;
  public paginatorEvent: PageEvent;
  public maxProductPrice: number;
  public displayedColumns = ['card'];
  public dataSource = new MatTableDataSource<Product>();
  @ViewChild('paginator') paginator: MatPaginator;

  constructor(private productService: ProductService, private eventService: FilterEventService, public dialog: MatDialog) { }


  public loadProducts() {
    this.subscribtionInfo =
    this.productService.getFilteredProducts(this.productCategoriesFilter,
      this.productNameFilter,
      this.productPriceFilter,
      this.productIncludeOutOfStockFilter,
      this.paginatorEvent?.pageIndex,
      this.paginatorEvent?.pageSize
      ).pipe(first()).subscribe((data: ProductResponse) => {
          this.paginator.pageIndex = data.pageNumber;
          this.dataSource.data = data.products;
          this.paginator.length = data.productCount;
          this.maxProductPrice = data.maxProductPrice;

      });
  }
  public openDialog(){
        const dialogRef = this.dialog.open(AddProductComponent, {
          data: this.product
      });
      dialogRef.afterClosed().subscribe((result: Product) => {
        if(result){                            
            this.productService.addProduct(result).subscribe(data => this.loadProducts());
            this.product = new Product();
        }
    });
  }
  public ngOnInit() {
    this.product = new Product();
  }

  public ngAfterViewInit() {
    this.loadProducts();
  }

  public ngOnDestroy() {
    this.subscribtionInfo.unsubscribe();
  }

  public getNameFilter(nameInputString: string) {
    this.productNameFilter = nameInputString;
  }
  public getIncludeOutOfStock(includeOutOfStock: boolean) {
    this.productIncludeOutOfStockFilter = includeOutOfStock;
  }
  public getPriceFilter(productPriceFilter: number) {
    this.productPriceFilter = productPriceFilter;
  }
  public getCategoriesFilter(categories: string[]) {
    this.productCategoriesFilter = categories;
    this.loadProducts();
  }

  public deleteProduct(productId: number) {
    this.productService.deleteProduct(productId).subscribe(data => {

      this.loadProducts();

    });
  }
}
