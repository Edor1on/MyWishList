using Microsoft.EntityFrameworkCore;
using MyWishList.Shared.Models;

namespace MyWishList.API.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Goal> Goals { get; set; }
    }
}
