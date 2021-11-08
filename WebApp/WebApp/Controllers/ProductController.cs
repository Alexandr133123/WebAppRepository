using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.BusinessLogicLayer.IServices;
using AutoMapper;
using WebApp.PresentationLayer.DTO;
using Microsoft.Extensions.Configuration;
using WebApp.DataAccessLayer.Model;
using Serilog;
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
            var serviceResult = service.GetProducts(filters, parameners);
            var mappedProducts = mapper.Map<ViewProductResponse>(serviceResult);
            return Ok(mappedProducts);
        }

        [HttpPut]
        public IActionResult UpdateProduct(ViewProduct viewProduct)
        {
                var product = mapper.Map<Product>(viewProduct);
                service.UpdateProducts(product);                
                return Ok();            
        }
        [HttpPost]
        public IActionResult AddProduct(ViewProduct viewProduct)
        {
            var product = mapper.Map<Product>(viewProduct);
            service.AddProduct(product);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            service.DeleteProduct(id);
            return Ok();
        }
        [HttpGet]
        [Route("chart")]
        public IActionResult GetChartData()
        {
            return Ok(service.GetChartChartData());
        }
    }
}
