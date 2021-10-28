﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DataAccessLayer.Model;
using WebApp.PresentationLayer.DTO;

namespace WebApp.DataAccessLayer.IRepository
{
    public interface IProductRepository
    {

        IQueryable<Product> GetProducts();
        IQueryable<Product> GetProducts(Filters filters);

    }
}
