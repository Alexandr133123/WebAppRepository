import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { first } from "rxjs/operators";
import { Category } from "src/app/shared/models/Category";
import { Product } from "src/app/shared/models/Product";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})

export class ProductService {


    private url = environment.apiUrl + 'product';

    constructor(private http: HttpClient) { }

    public getFilteredProducts(categories: string[],
        productName: string, priceTo: number = 0,
        includeOutOfStock: boolean, pageNumber: number | undefined,
        pageSize: number | undefined = 5) {

        let params = new HttpParams();
        if (pageNumber != undefined && pageNumber >= 0) {
            params = params.append(`pageNumber`, pageNumber);
        }
        if (pageSize != undefined && pageSize != 0) {
            params = params.append(`pageSize`, pageSize)
        }
        if (productName != undefined && productName.length != 0) {
            params = params.append(`productName`, productName.replace(/\s+/g, ''));
        }
        if (priceTo > 0) {
            params = params.append(`priceTo`, priceTo);
        }
        if (includeOutOfStock) {
            params = params.append(`includeOutOfStock`, includeOutOfStock);
        }
        if (categories != undefined) {
            categories.forEach((s: string) => {
                params = params.append(`category`, s);
            });
        }

        return this.http.get(this.url,{
            params: params          
        });

    }
    public updateProduct(product: Product, file: File) {
        const data = new FormData();
          data.append('productString', JSON.stringify(product));
          data.append("uploadedFile",file);

        return this.http.put(this.url, data, );
    }
    public addProduct(product: Product, file: File) {
        const data = new FormData();
          data.append('productString', JSON.stringify(product));
          data.append("uploadedFile",file);
        return this.http.post(this.url, data);
    }
    public deleteProduct(id: number) {

        return this.http.delete(this.url + '/' + id);
    }
    public getProductChartInfo(){
        return this.http.get(this.url + '/chart');
    }
}