using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SBTBackEnd.Entities.Product;
using SBTBackEnd.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProductsAsync()
        {
            return Ok(await _productService.GetProductsAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProductById(int id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpGet("GetSubProduct")]
        public async Task<ActionResult<List<SubProduct>>> GetSubProducts()
        {
            return Ok(await _productService.GetSubProducts());
        }
    }
}
