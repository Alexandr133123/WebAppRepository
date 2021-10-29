using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.PresentationLayer.DTO
{
    public class ProductParameters
    {
        private int _maxPageSize;
        public int MaxPageSize
        {
            get
            {
                return _maxPageSize;
            }
            set
            {
                _maxPageSize = value;
                PageSize = (PageSize > _maxPageSize) ? _maxPageSize : PageSize;
            }
        }
        [FromQuery(Name ="pageNumber")]
        public int PageNumber { get; set; } = 0;
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; }
 
    }
}
