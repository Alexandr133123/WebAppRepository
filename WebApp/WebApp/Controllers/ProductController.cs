using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.BusinessLogicLayer.IServices;
using AutoMapper;
using WebApp.PresentationLayer.DTO;
namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       private IProductService service;
       private IMapper mapper;

        public ProductController(IProductService service,IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ViewProduct> GetProducts()
        {

            return mapper.Map<IEnumerable<ViewProduct>>(service.GetProducts());
           
        }

        [HttpGet]
        [Route("filter")]
        public IEnumerable<Product> GetFilteredProducts([FromQuery] Filters filters )
        {
            return service.GetProducts(filters);
        }


    }
}
