using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.DB;
using WebApp.DataAccessLayer.IRepository;
using WebApp.PresentationLayer.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using WebApp.DataAccessLayer.Model;

namespace WebApp.DataAccessLayer.Repository
{
    public class ProcedureManager: IProcedureManager
    {
        private ApplicationContext db;
        public ProcedureManager(ApplicationContext db)
        {
            this.db = db;
        }
        
        public List<T> ExecuteProcedure<T>(ProdcedureParameters parameters) where T: class
        {
            var query= db.Set<T>().FromSqlRaw(
               @"EXECUTE dbo.ProductInfoReport2 @CategoryIds, @StartDate,@EndDate,@IncludeOutOfStock,@GroupByMode",
               new SqlParameter("CategoryIds", parameters.CategoryIds),
               new SqlParameter("StartDate", parameters.DateFrom),
               new SqlParameter("EndDate", parameters.DateTo),
               new SqlParameter("IncludeOutOfStock", 1),
               new SqlParameter("GroupByMode", parameters.GroupByMode)).ToList();
            return query;
          
         
		}
    }
}
