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
            if (await _context.Database.CanConnectAsync())
            {
                if (!_context.Products.Any())                                  //sprawdzamy czy jest pusta
                {

                    var products = new List<Product>
                {
                   new Product { Name= "Phone", Ean="1234"},
                   new Product { Name= "Computer", Ean="4567" },
                   new Product { Name="Headphones", Ean="8910" }
                };
                    _context.Products.AddRange(products);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
