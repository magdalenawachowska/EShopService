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
        Product GetById(int id);
        IEnumerable<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);

        #region Product 
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product user);
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductAsync(int id);
        #endregion
    }
}
