using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Repositories;
using EShop.Domain.Models;

namespace EShop.Domain.Seeders
{
    public class EShopSeeder : IEShopSeeder    //static
    {
        private readonly DataContext _context;

        public EShopSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {

            if (!_context.Products.Any())                               
                {
                    var products = new List<Product>
                    {
                       new Product { Name= "Phone", Ean="1234", Price=799.99m},
                       new Product { Name= "Computer", Ean="4567", Price=4999.99m },
                       new Product { Name="Headphones", Ean="8910", Price=349.99m }
                    };
                    _context.Products.AddRange(products);
                    await _context.SaveChangesAsync();
                }
        }
    }
}
