using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.BusinessLogicLayer.IServices;
using AutoMapper;
using WebApp.PresentationLayer.DTO;
using WebApp.PresentationLayer.Wrapper;
using Microsoft.Extensions.Configuration;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public ProductController(IProductService service, IMapper mapper, IConfiguration configuration)
        {
            this.service = service;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetFilteredProducts([FromQuery] Filters filters, [FromQuery] ProductParameters parameners)
        {
            parameners.MaxPageSize = Int32.Parse(configuration["Pagination:MaxPageSize"]);
            var query = service.GetProducts(filters);
            var resultCount = query.Count();
            query = query
            .Skip((parameners.PageNumber * parameners.PageSize))
            .Take(parameners.PageSize);
            var result = mapper.Map<List<ViewProduct>>(query);
            decimal maxPrice = service.GetMaxPrice();
            return Ok(new ProductResponse(result, resultCount,maxPrice));
        }

    }
}
