using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;

namespace WebApp.DataAccessLayer.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationContext db;

        public CategoryRepository(ApplicationContext db)
        {
            this.db = db;
        }


        public IQueryable<Category> GetCategories(List<string> categories)
        {
            string sqlString = string.Join(";",categories);

            return db.Categories.FromSqlRaw(

                @"WITH R AS
                 (
	                SELECT 
		                PK_CategoryId,
		                CategoryName,
		                FK_ParentCategoryId
			                FROM
				                Category
			                WHERE CategoryName in (SELECT value FROM STRING_SPLIT(@CategoryNames,';'))
			                UNION ALL
			                SELECT
				                c.PK_CategoryId,
				                c.CategoryName,
				                c.FK_ParentCategoryId
			                FROM
			                   Category c
			                JOIN R rq
					                on rq.PK_CategoryId = c.FK_ParentCategoryId
                )select * from R", new SqlParameter("CategoryNames", sqlString));
    
        } 
		
		public IEnumerable<Category> GetCategories()
        {

            IQueryable<Category> categoryQuery = db.Categories.FromSqlRaw(

				@"WITH R AS
                 (
	                SELECT 
		                PK_CategoryId,
		                CategoryName,
		                FK_ParentCategoryId
			                FROM
				                Category
			                WHERE FK_ParentCategoryId is NUll
			                UNION ALL
			                SELECT
				                c.PK_CategoryId,
				                c.CategoryName,
				                c.FK_ParentCategoryId
			                FROM
			                   Category c
			                JOIN R rq
					                on rq.PK_CategoryId = c.FK_ParentCategoryId
                )select * from R");


			return categoryQuery.ToList().Where(c => c.FK_ParentCategory == null);
    
        }
    }
}
