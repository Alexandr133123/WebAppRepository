export class Product{

    constructor(
       public productId?: number,
       public productName?: string,
       public price?: number,
       public quantityInStock?: number,
       public lastModified?: Date,
       public ProductCategoriesId?: number[] 
    ){}
}