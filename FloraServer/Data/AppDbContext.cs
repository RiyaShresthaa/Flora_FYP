using FloraSharedLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace FloraServer.Data
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<UserAccount> UserAccounts { get; set; } = default!;
        public DbSet<SystemRole> SystemRoles { get; set; } = default!;
        public DbSet<UserRole> UserRoles { get; set; } = default!;
        public DbSet<TokenInfo> TokenInfo { get; set; } = default!;
    }
}