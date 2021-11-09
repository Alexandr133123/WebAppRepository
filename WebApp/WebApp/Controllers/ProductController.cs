using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApp.BusinessLogicLayer.IServices;
using AutoMapper;
using WebApp.PresentationLayer.DTO;
using Microsoft.Extensions.Configuration;
using WebApp.DataAccessLayer.Model;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetFilteredProductsAsync([FromQuery] Filters filters, [FromQuery] ProductParameters parameners)
        {
            parameners.MaxPageSize = Int32.Parse(configuration["Pagination:MaxPageSize"]);
            ProductResponse serviceResult = await service.GetProductsAsync(filters, parameners);
            var mappedProducts = mapper.Map<ViewProductResponse>(serviceResult);
            foreach(ViewProduct vp in mappedProducts.Products)
            {
                ProductImage imagePath = serviceResult.Products.Find(p => vp.ProductId == p.PK_ProductId).Image;
                if (imagePath != null)
                {
                    vp.Image = await service.SetProductImageAsync(imagePath.ImageRelativePath);
                }
                
            }
            return Ok(mappedProducts);
        }
        [HttpPut]
        public IActionResult UpdateProduct([FromForm] string productString, [FromForm] IFormFile uploadedFile)
        {            
            ViewProduct viewProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewProduct>(productString);
            var product = mapper.Map<Product>(viewProduct);
            if(uploadedFile == null)
            {                
                return Ok();
            }
            service.UpdateProductsAsync(product, uploadedFile);              
                return Ok();            
        }
        [HttpPost]
        public IActionResult AddProduct([FromForm] string productString,[FromForm] IFormFile uploadedFile)
        {
            ViewProduct viewProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewProduct>(productString);
            var product = mapper.Map<Product>(viewProduct);
            if (uploadedFile == null)
            {
                service.AddProduct(product);
                return Ok();
            }
            service.AddProductAsync(product,uploadedFile);
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
        public async Task<IActionResult> GetChartDataAsync()
        {
            return  Ok(await service.GetChartChartDataAsync());
        }
    }
}
