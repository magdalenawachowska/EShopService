﻿using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Repositories
{
    public interface IProductRepository                //interfejs repozytorium- definiuje metody do manipulacji danymi 
    {

        Task<Product> AddProductAsync(Product product);
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> UpdateProductAsync(Product product);

    }
}
