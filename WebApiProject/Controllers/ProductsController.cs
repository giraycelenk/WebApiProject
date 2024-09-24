using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.DTO;
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
        [Authorize]
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            
            var products = await _context
                                .Products
                                .Where(x => x.IsActive)
                                .Select(x => ProductToDTO(x))
                                .ToListAsync();

            return Ok(products);
        }
        [Authorize]
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var p = await _context
                            .Products
                            .Where(x => x.ProductId == id && x.IsActive)
                            .Select(x => ProductToDTO(x))
                            .FirstOrDefaultAsync();

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }
        [Authorize]
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct) , new {id = entity.ProductId},entity);
        }
        [Authorize]
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id,Product entity)
        {
            if(id != entity.ProductId)
            {
                return BadRequest();
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if(product == null)
            {
                return NotFound();
            }

            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
            
            return NoContent();
        }
        [Authorize]
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if(product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
            
            return NoContent();
        }
        private static ProductDTO ProductToDTO(Product p)
        {
            var entity = new ProductDTO();
            if(p != null)
            {
                entity.ProductId = p.ProductId;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }
            return entity;
        }
    }
}