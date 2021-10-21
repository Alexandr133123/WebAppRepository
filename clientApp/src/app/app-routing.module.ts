import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./features/home-page/home.component";
import { ProductComponent } from "./features/product-page/product.component";
import { HeaderComponent } from "./shared/layout/header/header.component";
import { LayoutModule } from "./shared/layout/layout.module";

const routes: Routes = [
    {path: '', redirectTo:'/product', pathMatch: 'full'},
    {path: 'product', component:HeaderComponent, children: [{path: '', component: ProductComponent}]},
    {path: 'home', component:HeaderComponent, children: [{path: '', component: HomeComponent}]}
];

@NgModule({
    imports: [RouterModule.forRoot(routes), LayoutModule],
    exports: [RouterModule]
})
export class AppRoutingModule { }