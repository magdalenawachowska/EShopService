﻿using EShop.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EShop.Domain.Repositories
{
    public class ProductRepository : IProductRepository                
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;                     //lub return true;
        }
    }
}
