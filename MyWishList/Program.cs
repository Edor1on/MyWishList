using Microsoft.EntityFrameworkCore;
using MyWishList.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Додаємо підтримку контролерів
builder.Services.AddControllers();

// 2. Реєструємо нашу базу даних SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 3. Базові налаштування безпеки
app.UseHttpsRedirection();
app.UseAuthorization();

// 4. НАЙГОЛОВНІШИЙ РЯДОК: Кажемо серверу слухати наші [Route] в контролерах
app.MapControllers();

app.Run();