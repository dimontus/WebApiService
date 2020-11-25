using Microsoft.EntityFrameworkCore;
using WebApiService.Repository;

namespace WebApiService
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductDbModel> Product { get; set; }

        public ProductContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}