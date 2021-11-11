import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ChartPageComponent } from "./features/chart-page/chart-page.component";
import { DownloadComponent } from "./features/download-page/download.component";
import { LoginPageComponent } from "./features/login-page/login-page.component";
import { ProductComponent } from "./features/product-page/product.component";
import { RegistrationPageComponent } from "./features/registration-page/registration-page.component";
import { LayoutModule } from "./shared/layout/layout.module";
import { MainLayoutComponent } from "./shared/layout/main-layout/main-layout.component";

const routes: Routes = [
    {path: '', redirectTo:'/login', pathMatch: 'full'},
    {path: 'product', component: MainLayoutComponent, children: [{path: '', component: ProductComponent}]},
    {path: 'download', component: MainLayoutComponent, children: [{path: '', component: DownloadComponent}]},
    {path: 'chart', component: MainLayoutComponent, children: [{path: '', component: ChartPageComponent}]},
    {path: 'login', component: MainLayoutComponent, children: [{path: '', component: LoginPageComponent}]},
    {path: 'registration', component: MainLayoutComponent, children: [{path: '', component: RegistrationPageComponent}]}

];

@NgModule({
    imports: [RouterModule.forRoot(routes), LayoutModule],
    exports: [RouterModule]
})
export class AppRoutingModule { }