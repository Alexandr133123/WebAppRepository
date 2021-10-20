using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;
using WebApp.PresentationLayer.MapperConfig;
using AutoMapper;
namespace WebApp.PresentationLayer.Mapper
{
    public class MapperHandler
    {
        MapperConfigManager configManager;
        IMapper mapper;
        public MapperHandler(MapperConfigManager configManager)
        {
            this.configManager = configManager;
            mapper = configManager.getConfig().CreateMapper();
            
        }
        public ICollection<ViewProduct> mapProduct(ICollection<Product> products)
        {
            return mapper.Map<ICollection<ViewProduct>>(products);
        }


    }
}
