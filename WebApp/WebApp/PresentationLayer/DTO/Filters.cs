using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.DataAccessLayer.Model;

namespace WebApp.PresentationLayer.DTO
{
    public class Filters
    {
        [FromQuery(Name = "includeOutOfStock")]
        public bool? IncludeOutOfStock { get; set; }

        public decimal? PriceFrom { get; set; } = 0;

        [FromQuery(Name = "priceTo")]
        public decimal? PriceTo { get; set; }

        [FromQuery(Name = "category")]
        public List<string> Categores { get; set; }

        [FromQuery(Name ="productName")]
        public string ProductName { get; set; }

    }
}
