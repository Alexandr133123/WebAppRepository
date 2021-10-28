﻿using AutoMapper;
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
                .ForMember("ProductCategoriesId", p => p.MapFrom(pv => pv.ProductCategories.Select(i => i.PfkCategoryId)))
                .ForMember(pf => pf.ProductId, p => p.MapFrom(pv => pv.PkProductId));


        }


    }

}