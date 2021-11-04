using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BusinessLogicLayer.IServices;
using WebApp.PresentationLayer.DTO;
using WebApp.DataAccessLayer.IRepository;
using WebApp.DataAccessLayer.Model;
using ServiceStack.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApp.BusinessLogicLayer.Services
{
    public class DownloadService: IDownloadService
    {
        private IProcedureManager procedureManager;
        private string csv;
        public DownloadService(IProcedureManager procedureManager) {
            this.procedureManager = procedureManager;
        }
        public byte[] GetCSVData(ProdcedureParameters parameters)
        {
            if (parameters.GroupByMode == 1)
            {
                csv = CsvSerializer.SerializeToCsv<ProductGroupByLastModified>
                    (procedureManager.ExecuteProcedure<ProductGroupByLastModified>(parameters));
               
            }
            else if (parameters.GroupByMode == 2)
            {
                csv = CsvSerializer.SerializeToCsv<ProductGroupByCategory>
                     (procedureManager.ExecuteProcedure<ProductGroupByCategory>(parameters));
                
            }
            else if (parameters.GroupByMode == 3)
            {
                csv = CsvSerializer.SerializeToCsv<ProductGroupByPrice>
                     (procedureManager.ExecuteProcedure<ProductGroupByPrice>(parameters));
                
            }

            if(csv != null)
            {
                byte[] arr = Encoding.ASCII.GetBytes(csv);                
                return arr;
            }
            else
            {
                throw new ArgumentException();
            }
            
        }
    }
}
