using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

namespace WebApp.BusinessLogicLayer.IServices
{
    public interface IDownloadService
    {
        byte[] GetCSVData(ProdcedureParameters parameters);
    }
}
