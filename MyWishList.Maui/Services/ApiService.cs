using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using MyWishList.Shared.Models;

namespace MyWishList.Maui.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://localhost:7248/api/goals";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Goal>> GetGoalsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Goal>>(BaseUrl);
                return response ?? new List<Goal>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send error: {ex}");
                return new List<Goal>();
            }
        }

        public async Task<bool> PostGoalAsync(Goal goal)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, goal);

            if (!response.IsSuccessStatusCode)
            {
                // Читаємо повідомлення про помилку від бекенду
                var errorText = await response.Content.ReadAsStringAsync();

                // Виводимо його у вікно Output у Visual Studio
                System.Diagnostics.Debug.WriteLine($"================");
                System.Diagnostics.Debug.WriteLine($"API ERROR: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"DETAILS: {errorText}");
                System.Diagnostics.Debug.WriteLine($"================");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateGoalAsync(Goal goal)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{goal.Id}", goal);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGoalAsync(Goal goal)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{goal.Id}");
            return response.IsSuccessStatusCode;
        }


    }
}
