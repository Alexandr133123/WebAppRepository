using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;
namespace WebApp.PresentationLayer.MapperConfig
{
    public static class MapperConfigManager
    {
        private static MapperConfiguration MapperConfig = new MapperConfiguration(mc =>
        {
            mc.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            mc.AddProfile(new MapperProfile());

        });
    
        public static MapperConfiguration GetConfig()
        {
            return MapperConfig;
        }
    }

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ViewProduct>()
                .ForMember(c => c.ProductCategoriesId, p => p.MapFrom(pv => pv.Categories.Select(i => i.PK_CategoryId)))
                .ForMember(pf => pf.ProductId, p => p.MapFrom(pv => pv.PK_ProductId));
            CreateMap<Category, ViewCategory>()
                .ForMember(vc => vc.CategoryId, c => c.MapFrom(pc => pc.PK_CategoryId))
                .ForMember(vc => vc.ParentCategory, c => c.MapFrom(pc => pc.InverseFkParentCategory))
                .ForMember(vc => vc.ParentCategoryId, c => c.MapFrom(pc => pc.FK_ParentCategoryId));

        }


    }

}
