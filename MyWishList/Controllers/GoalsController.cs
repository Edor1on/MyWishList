using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWishList.Shared.Models;
using MyWishList.API.Infrastructure;

namespace MyWishList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Dependency Injection (DI) в дії: 
        // ASP.NET сам передає нам готове підключення до бази даних.
        public GoalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/wishes
        // Метод для отримання всього списку бажань
        [HttpGet]
        public async Task<IActionResult> GetGoals()
        {
            var goals = await _context.Goals.ToListAsync();
            return Ok(goals);
        }

        // POST: api/goals
        // Метод для створення нового бажання
        [HttpPost]
        public async Task<IActionResult> CreateGoal(Goal goal)
        {
            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGoals), new { id = goal.Id }, goal);
        }

        // PUT: api/wishes/5
        // Метод для оновлення існуючого бажання
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoals(Guid id, Goal updatedGoal)
        {
            if (id != updatedGoal.Id)
            {
                return BadRequest();
            }

            _context.Entry(updatedGoal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Goals.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoal(Guid id)
        {
            var goal = await _context.Goals.FindAsync(id);
            if (goal == null)
            {
                return NotFound();
            }

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
