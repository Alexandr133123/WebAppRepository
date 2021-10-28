export class Category
{
    categoryId: number;
    categoryName: string;
    parentCategoryId: number | null;
    parentCategory: Category[]; 
}