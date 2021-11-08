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
            foreach(ViewProduct vp in mappedProducts.Products)
            {
                if(serviceResult.Products.Find(p => vp.ProductId == p.PK_ProductId).Image != null)
                {
                    var imagePath = serviceResult.Products.Find(p => vp.ProductId == p.PK_ProductId).Image.ImageRelativePath;
                    vp.Image = service.SetProductImage(imagePath);
                }
                
            }
            return Ok(mappedProducts);
        }
        [HttpPut]
        public IActionResult UpdateProduct([FromForm] string productString, [FromForm] IFormFile uploadedFile)
        {
            ViewProduct viewProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewProduct>(productString);
            var product = mapper.Map<Product>(viewProduct);
            service.UpdateProducts(product, uploadedFile);              
                return Ok();            
        }
        [HttpPost]
        public IActionResult AddProduct([FromForm] string productString,[FromForm] IFormFile uploadedFile)
        {
            ViewProduct viewProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewProduct>(productString);
            var product = mapper.Map<Product>(viewProduct);
            service.AddProduct(product,uploadedFile);
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
