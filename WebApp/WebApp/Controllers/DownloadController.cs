using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private IDownloadService service;
        public DownloadController(IDownloadService service)
        {
            this.service = service;
        }

        [HttpPost]
        public FileResult DownloadParameters([FromBody] ProdcedureParameters parameters)
        {
            byte[] fileContent =  service.GetCSVData(parameters);
            string file_type = "application/csv";
            string file_name = "products.csv";
            
            Response.Headers.Add(HeaderNames.AccessControlExposeHeaders, HeaderNames.ContentDisposition);

            return File(fileContent, file_type, file_name);
        }
    }
}
