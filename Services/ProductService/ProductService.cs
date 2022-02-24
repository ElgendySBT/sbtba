using Microsoft.EntityFrameworkCore;
using SBTBackEnd.Data;
using SBTBackEnd.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(SP => SP.subProduct).FirstOrDefaultAsync(P => P.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(SP => SP.subProduct).ToListAsync();
        }

        public async Task<IReadOnlyList<SubProduct>> GetSubProducts()
        {
            return await _context.SubProducts.ToListAsync();
        }
    }
}
