using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.PresentationLayer.Mapper;
using WebApp.PresentationLayer.DTO;
namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService service;
        MapperHandler mapper;

        public ProductController(IProductService service,MapperHandler mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public ICollection<ViewProduct> GetViewProducts()
        {

            return mapper.mapProduct(service.GetProductsFromRep());
           
        }

        

    }
}
