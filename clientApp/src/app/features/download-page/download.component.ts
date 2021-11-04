import { Component } from "@angular/core";
import { DownloadPageService } from "./service/download-page.service";
import { AngularCsv } from 'angular-csv-ext/dist/Angular-csv';
import { MatAnchor } from "@angular/material/button";
import { saveAs } from 'file-saver';
import * as FileSaver from "file-saver";
import { HttpHeaders, HttpResponse } from "@angular/common/http";
@Component({

    selector: 'download-comp',
    templateUrl: './download.component.html',
    styleUrls:['./download.component.scss']
})

export class DownloadComponent {

    public groupByMode: number;
    public categoryIds: string;
    public dateFrom: Date;
    public dateTo: Date;
    public includeOutOfStock: boolean = false;

    constructor(private downloadService: DownloadPageService){
    }

    public sendData(ids: string[]){
        if(ids && ids.length !== 0 && this.groupByMode &&  this.dateFrom && this.dateTo){
            this.categoryIds = ids.join(';');
            this.downloadService.sendDownloadFilters(this.groupByMode,this.categoryIds,this.dateFrom,this.dateTo, this.includeOutOfStock).subscribe((data: HttpResponse<Blob>) => {this.downloadFile(data)}, error => console.log('failed'));
            
        }
    }   

    private downloadFile(data: HttpResponse<Blob>){

         var headerParameters = data.headers.get('content-disposition')?.match(/(?!filename=)+([a-z]*).(csv)(?=;)/);
         if(headerParameters![0]){
             var name = headerParameters![0];
            FileSaver.saveAs(data.body!, name);
         }else{
             alert('Error: Incorrect File Name');
         }
    }
 }