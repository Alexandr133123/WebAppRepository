import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ChartPageComponent } from "./features/chart-page/chart-page.component";
import { DownloadComponent } from "./features/download-page/download.component";
import { ProductComponent } from "./features/product-page/product.component";
import { LayoutModule } from "./shared/layout/layout.module";
import { MainLayoutComponent } from "./shared/layout/main-layout/main-layout.component";

const routes: Routes = [
    {path: '', redirectTo:'/product', pathMatch: 'full'},
    {path: 'product', component:MainLayoutComponent, children: [{path: '', component: ProductComponent}]},
    {path: 'download', component:MainLayoutComponent, children: [{path: '', component: DownloadComponent}]},
    {path: 'chart', component:MainLayoutComponent, children: [{path: '', component: ChartPageComponent}]},

];

@NgModule({
    imports: [RouterModule.forRoot(routes), LayoutModule],
    exports: [RouterModule]
})
export class AppRoutingModule { }