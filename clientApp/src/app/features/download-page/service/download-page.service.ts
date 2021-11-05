import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'

})

export class DownloadPageService{
    private url = environment.apiUrl + 'download';
  
    constructor(private http: HttpClient){ }

    public sendDownloadFilters( 
        groupByMode: number, categoryIds: 
        string, dateFrom: Date, dateTo: Date, includeOutOfStock: boolean){

            const body = {categoryIds: categoryIds, 
                groupByMode: groupByMode, 
                dateFrom: dateFrom, dateTo:dateTo, 
                includeOutOfStock:includeOutOfStock}
        
        return this.http.post(this.url, body, {responseType: 'blob', observe:'response'});
    }
}