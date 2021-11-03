export class Category
{
    categoryId: number;
    categoryName: string;
    parentCategoryId: number;
    parentCategory: Category[]; 
}