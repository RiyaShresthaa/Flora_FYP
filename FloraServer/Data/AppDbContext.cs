using FloraSharedLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FloraServer.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext
    {
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Catetgories { get; set; } = default!;

    }
}
