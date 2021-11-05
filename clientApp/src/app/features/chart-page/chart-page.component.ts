import { Component, OnInit, ViewChild } from "@angular/core";
import * as ApexCharts from "apexcharts";
import { ApexAxisChartSeries, ApexChart, ApexXAxis, ApexDataLabels, ApexGrid, ApexStroke, ApexTitleSubtitle, ChartComponent, ApexFill, ApexMarkers, ApexTooltip, PointAnnotations, ChartType,  } from "ng-apexcharts";
import { first } from "rxjs/operators";
import { Product } from "src/app/shared/models/Product";
import { ProductService } from "../../shared/service/product.service";
import { ChartDTO } from "./models/ChartDTO";

export type ChartOptions = {
    markers: ApexMarkers;
    annotation: PointAnnotations;
    fill: ApexFill;
    series: ApexAxisChartSeries | ApexNonAxisChartSeries;
    chart: ApexChart;
    xaxis: ApexXAxis;
    dataLabels: ApexDataLabels;
    responsive: ApexResponsive[],
    grid: ApexGrid;
    stroke: ApexStroke;
    title: ApexTitleSubtitle;
    labels: any;
  };


@Component({
    selector: 'chart-comp',
    templateUrl: './chart-page.component.html'
})
export class ChartPageComponent implements OnInit{
    @ViewChild("chart") chart: ChartComponent;
    public chartOptions: Partial<ChartOptions>;
    public chartType: ChartType;
    private productCount:number[];
    private productPrice:number[];
    
    constructor(private productService: ProductService) {
        this.chartType = "line"      
    }

    ngOnInit(){
        this.loadChartData();
    }
    public chartModeSelect(chartType: ChartType){
        this.chartType = chartType;
        this.buildChart();
    }
    private loadChartData(){
        this.productService.getProductChartInfo().pipe(first()).subscribe((data: ChartDTO) => {this.productCount = data.productCount, this.productPrice = data.productPrice; this.buildChart();});
    }
    private buildLineOrBarChart(): Partial<ChartOptions>{
        return  {
            markers:{
                colors: ["#673ab7"]
            },
            fill:{
                colors: ["#673ab7"]
            },
            
            series: [
              {
                name: "Product Count",
                color: "#673ab7",
                data: this.productCount                               
              }
            ],
            chart: {
              height: 350,
              type: this.chartType,
              zoom: {
                enabled: false
              },
           
            },
            dataLabels: {
              enabled: false,
                                
            },
            stroke: {
              curve: "straight",
              colors: ["#673ab7"]
            },
            labels: this.productPrice,
            title: {
              text: "Product Count/Price Relation",
              align: "left"
            },          
            xaxis: {
              categories: this.productPrice
            }
        
          };
    }
    private buildPieChart(): Partial<ChartOptions>{
        return {
            chart: {
              height: 350,
              type: this.chartType,
           
            },
            series: this.productCount,
            labels: this.productPrice,         
            responsive: [
                {
                  breakpoint: 480,
                  options: {
                    chart: {
                      width: 200
                    },
                    legend: {
                      position: "bottom"
                    }
                  }
                }
              ]
        
          };
    }
    public buildChart(){
           if(this.chartType != "pie"){
            this.chartOptions = this.buildLineOrBarChart();
           }else{
            this.chartOptions = this.buildPieChart();
           }
         
        
    }
}