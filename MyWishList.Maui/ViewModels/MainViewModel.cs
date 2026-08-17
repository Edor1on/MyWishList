using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using MyWishList.Maui.Services;
using MyWishList.Shared.Models;

namespace MyWishList.Maui.ViewModels
{
    public class MainViewModel : BindableObject
    {
        private readonly ApiService _apiService;
        private string _newWishName;

        public ObservableCollection<Wish> Wishes { get; set; } = new ObservableCollection<Wish>();

        public string NewWishName
        {
            get => _newWishName;
            set
            {
                _newWishName = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadWishesCommand { get; }
        public ICommand AddWishesCommand { get; }

        public MainViewModel(ApiService apiService)
        {
            _apiService = apiService;

            LoadWishesCommand = new Command(async () => await LoadWishes());
            AddWishesCommand = new Command(async () => await AddWishes());

            LoadWishesCommand.Execute(null);
        }

        private async Task LoadWishes()
        {
            var wishesFromDb = await _apiService.GetWishesAsync();
            Wishes.Clear();
            foreach (var wish in wishesFromDb)
            {
                Wishes.Add(wish);
            }
        }

        [Obsolete]
        private async Task AddWishes()
        {
            if (string.IsNullOrWhiteSpace(NewWishName))
            {
                return;
            }

            var newWish = new Wish
            {
                Name = NewWishName,
                Description = "Created by MAUI app"
            };

            var success = await _apiService.PostWishesAsync(newWish);
            if (success)
            {
                NewWishName = string.Empty;
                await LoadWishes();
            }
            else
            {
                // Якщо сервер повернув помилку, показуємо спливаюче вікно!
                await Application.Current.MainPage.DisplayAlert("Помилка", "Сервер відхилив запит! Перевір Output у Visual Studio.", "ОК");
            }
        }
    }
}
