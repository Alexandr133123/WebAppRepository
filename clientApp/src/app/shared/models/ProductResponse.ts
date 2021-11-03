import { Product } from "./Product";

export class ProductResponse{
        public productCount: number;
        public maxProductPrice: number;
        public products: Product[];
        public pageNumber:number;
}