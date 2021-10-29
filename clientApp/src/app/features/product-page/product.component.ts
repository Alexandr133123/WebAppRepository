import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ProductService } from './service/product.service';
import { Product } from '../../shared/models/Product';
import { FilterEventService } from './service/filter-event.service';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { first } from 'rxjs/operators';
import { ProductResponse } from 'src/app/shared/models/ProductResponse';
@Component({
  selector: 'product-comp',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})

export class ProductComponent implements OnInit, AfterViewInit, OnDestroy {
  private productNameFilter: string;
  private subscribtionInfo: Subscription;
  private productCategoriesFilter: string[]
  private productPriceFilter: number;
  private productIncludeOutOfStockFilter: boolean;
  public paginatorEvent: PageEvent;
  public maxProductPrice: number;
  public displayedColumns = ['card'];
  public dataSource = new MatTableDataSource<Product>();
  @ViewChild('paginator') paginator: MatPaginator;

  constructor(private productService: ProductService, private eventService: FilterEventService) { }


  public loadProducts() {
    this.subscribtionInfo =
    this.productService.GetFilteredProducts(this.productCategoriesFilter,
      this.productNameFilter,
      this.productPriceFilter,
      this.productIncludeOutOfStockFilter,
      this.paginatorEvent?.pageIndex,
      this.paginatorEvent?.pageSize
      ).pipe(first()).subscribe((data: ProductResponse) => {
          this.dataSource.data = data.products;
          this.paginator.length = data.productCount;
          this.maxProductPrice = data.maxProductPrice;

      });
  }

  public ngOnInit() {
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

  public onDeleteAction(productId: number) {
    alert(productId);
  }
}
