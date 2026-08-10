using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWishList.API.Domain.Entities;
using MyWishList.API.Infrastructure;

namespace MyWishList.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class WishesController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Dependency Injection (DI) в дії: 
        // ASP.NET сам передає нам готове підключення до бази даних.
        public WishesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/wishes
        // Метод для отримання всього списку бажань
        [HttpGet]
        public async Task<IActionResult> GetWishes()
        {
            var wishes = await _context.Wishes.ToListAsync();
            return Ok(wishes);
        }

        // POST: api/wishes
        // Метод для створення нового бажання
        [HttpPost]
        public async Task<IActionResult> CreateWish(Wish wish)
        {
            _context.Wishes.Add(wish);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWishes), new { id = wish.Id }, wish);
        }
    }
}
