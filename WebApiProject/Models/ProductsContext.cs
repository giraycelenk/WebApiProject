using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApiProject.Models
{
    public class ProductsContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public ProductsContext(DbContextOptions<ProductsContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 1, ProductName = "IPhone 14",Price=46000, IsActive = true});
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 2, ProductName = "IPhone 15",Price=56000, IsActive = true});
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 3, ProductName = "IPhone 16",Price=66000, IsActive = true});
            modelBuilder.Entity<Product>().HasData(new Product() { ProductId = 4, ProductName = "IPhone 17",Price=76000, IsActive = false});
        }

        public DbSet<Product> Products { get; set; }
        
    }
}