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

        private const string BaseUrl = "https://localhost:7248/api/wishes";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Wish>> GetWishesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Wish>>(BaseUrl);
                return response ?? new List<Wish>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Send error: {ex}");
                return new List<Wish>();
            }
        }

        public async Task<bool> PostWishesAsync(Wish wish)
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, wish);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateWishAsync(Wish wish)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{wish.Id}", wish);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteWishesAsync(Wish wish)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{wish.Id}");
            return response.IsSuccessStatusCode;
        }


    }
}
