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
                .ForMember(vp => vp.ProductId, m => m.MapFrom(p => p.PK_ProductId));
            CreateMap<Category, ViewCategory>()
                .ForMember(vc => vc.CategoryId, c => c.MapFrom(pc => pc.PK_CategoryId))
                .ForMember(vc => vc.ParentCategory, c => c.MapFrom(pc => pc.InverseFkParentCategory))
                .ForMember(vc => vc.ParentCategoryId, c => c.MapFrom(pc => pc.FK_ParentCategoryId));
            CreateMap<ProductResponse, ViewProductResponse>()
                .ForMember(pr => pr.Products, p => p.MapFrom(pc => pc.Products));
            CreateMap<Category, ViewProductCategory>();

            CreateMap<ViewProduct, Product>()
                .ForMember(p => p.PK_ProductId, m => m.MapFrom(vp => vp.ProductId))
                .ForMember(p => p.Categories, m => m.MapFrom(vp => vp.Categories));
            CreateMap<ViewCategory, Category>()
              .ForMember(vc => vc.PK_CategoryId, c => c.MapFrom(pc => pc.CategoryId))
              .ForMember(vc => vc.InverseFkParentCategory, c => c.MapFrom(pc => pc.ParentCategory))
              .ForMember(vc => vc.FK_ParentCategoryId, c => c.MapFrom(pc => pc.ParentCategoryId))
              .ForMember(vc => vc.FK_ParentCategory, m => m.Ignore());
            

        }


    }

}
