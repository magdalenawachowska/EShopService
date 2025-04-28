using EShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Repositories
{
    public interface IProductRepository                //interfejs repozytorium- definiuje metody do manipulacji danymi 
    {

        #region Product 
        Task<Product> GetProductAsync(int id);
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product user);
        Task<List<Product>> GetAllProductAsync();
        
        #endregion
    }
}
