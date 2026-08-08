using Microsoft.EntityFrameworkCore;
using MyWishList.API.Domain.Entities;

namespace MyWishList.API.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Wish> Wishes { get; set; }
    }
}
