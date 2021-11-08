using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;

namespace WebApp.PresentationLayer.DTO
{
    public class ViewCategory
    {           
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public int? ParentCategoryId { get; set; }
            public ICollection<ViewCategory> Children { get; set; }
    }
}
