using Microsoft.Extensions.Logging;
using MyWishList.Maui.Services;

namespace MyWishList.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            // Реєструємо HttpClient для відправки запитів
            builder.Services.AddSingleton<HttpClient>();

            // Реєструємо наш створений ApiService
            builder.Services.AddSingleton<ApiService>();

            // Реєструємо ViewModel та саму сторінку як Transient (створюються заново при кожному відкритті)
            builder.Services.AddTransient<MyWishList.Maui.ViewModels.MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
