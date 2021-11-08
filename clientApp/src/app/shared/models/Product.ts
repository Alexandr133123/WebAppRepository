import { Byte } from "@angular/compiler/src/util";
import { Category } from "./Category";

export class Product{
     productId: number;
     productName: string;
     price: number;
     quantityInStock: number;
     categories: Category[];
     image: ArrayBuffer;
}