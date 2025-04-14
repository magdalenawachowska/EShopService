using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Repositories;
using EShop.Domain.Models;
using EShopDomain.Models;

namespace EShop.Domain.Seeders
{
    public class EShopSeeder (DataContext context) : IEShopSeeder    //static
    {

        //public async Task Seed(DataContext context)
        public static void Seed()
        //public async Seed()
        {
            if (!context.Products.Any())                                  //o co chodzi ze nie moze byc null? /wskrzykiwanie zaleznosci??
            {
                
                var products = new List<Product>
                {
                   new Product { Name= "Phone", Ean="1234"},
                   new Product { Name= "Computer", Ean="4567" },
                   new Product { Name="Headphones", Ean="8910" }
                };
                context.Products.AddRange(products);
                /*
                context.Products.AddRange(
                    new Product { Name = "Phone", Ean="1234" },           //nie ustawiam id, bo EF Core go wygeneruje automatycznie!
                    new Product { Name = "Earphones", Ean="431" },       //o ile kolumna id jest Identity 
                    new Product { Name = "TV", Ean="12212" }
                    );
                */
                context.SaveChanges();

            }
        }
    }
}
