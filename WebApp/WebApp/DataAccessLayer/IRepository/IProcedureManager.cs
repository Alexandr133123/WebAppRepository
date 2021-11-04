using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

namespace WebApp.DataAccessLayer.IRepository
{
    public interface IProcedureManager
    {
        List<T> ExecuteProcedure<T>(ProdcedureParameters parameters) where T: class;
    }
}
