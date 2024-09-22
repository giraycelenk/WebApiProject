using Microsoft.AspNetCore.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private static List<Product>? _products;
        
        public ProductsController()
        {
            _products =
            [
                new Product{ ProductId = 1, ProductName = "IPhone 14",Price=46000, IsActive = true},
                new Product{ ProductId = 2, ProductName = "IPhone 15",Price=56000, IsActive = true},
                new Product{ ProductId = 3, ProductName = "IPhone 16",Price=66000, IsActive = true},
                new Product{ ProductId = 4, ProductName = "IPhone 17",Price=76000, IsActive = false},
            ];
        }
        
        [HttpGet]
        public IActionResult GetProducts()
        {
            if(_products == null)
            {
                return NotFound();
            }

            return Ok(_products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var p = _products?.FirstOrDefault(x => x.ProductId == id);

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }
    }
}