export interface Product{
     productId: number;
     productName: string;
     price: number;
     quantityInStock: number;
     lastModified: Date;
     ProductCategoriesId?: number[]; 
}