using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly ProductsContext _context;
        
        public ProductsController(ProductsContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            
            var products = await _context.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var p = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct) , new {id = entity.ProductId},entity);
        }
    }
}