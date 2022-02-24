using SBTBackEnd.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Services.ProductService
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductById(int id);
        Task<IReadOnlyList<SubProduct>> GetSubProducts();
    }
}
