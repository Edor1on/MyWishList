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
        private string _newGoalName;

        public ObservableCollection<Goal> Goals { get; set; } = new ObservableCollection<Goal>();

        public string NewGoalName
        {
            get => _newGoalName;
            set
            {
                _newGoalName = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadGoalsCommand { get; }
        public ICommand AddGoalCommand { get; }
        public ICommand UpdateGoalCommand { get; }
        public ICommand DeleteGoalCommand { get; }

        public MainViewModel(ApiService apiService)
        {
            _apiService = apiService;

            LoadGoalsCommand = new Command(async () => await LoadGoals());
            AddGoalCommand = new Command(async () => await AddGoal());
            UpdateGoalCommand = new Command<Goal>(async (goal) => await UpdateGoal(goal));
            DeleteGoalCommand = new Command<Goal>(async (goal) => await DeleteGoal(goal));

            LoadGoalsCommand.Execute(null);
        }

        private async Task LoadGoals()
        {
            var goalsFromDb = await _apiService.GetGoalsAsync();
            Goals.Clear();
            foreach (var goal in goalsFromDb)
            {
                Goals.Add(goal);
            }
        }

        private async Task AddGoal()
        {
            if (string.IsNullOrWhiteSpace(NewGoalName))
            {
                return;
            }

            var newGoal = new Goal
            {
                Name = NewGoalName,
                Description = "Created by MAUI app"
            };

            var success = await _apiService.PostGoalAsync(newGoal);
            if (success)
            {
                NewGoalName = string.Empty;
                await LoadGoals();
            }
            else
            {
                // Якщо сервер повернув помилку, показуємо спливаюче вікно!
                await Application.Current.MainPage.DisplayAlert(
                    "Помилка",
                    "Сервер відхилив запит! Перевір Output у Visual Studio.",
                    "ОК");
            }
        }

        private async Task UpdateGoal(Goal goal)
        {
            if (goal == null)
            {
                return;
            }

            var success = await _apiService.UpdateGoalAsync(goal);

            if (success)
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert(
                    "Success",
                    "Your Goal was updated successfuly",
                    "Ok");
            }
            else
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert(
                    "Error",
                    "Your Goal was not updated",
                    "Ok");
            }
        }

        private async Task DeleteGoal(Goal goal)
        {
            if (goal == null)
            {
                return;
            }

            bool answer = await Application.Current!.Windows[0].Page!.DisplayAlert(
                "Confirmation",
                $"Are you sure for delete the '{goal.Name}'?",
                "Yes", "No");

            if (!answer)
            {
                return;
            }

            var success = await _apiService.DeleteGoalAsync(goal);

            if (success)
            {
                Goals.Remove(goal);
            }
            else
            {
                await Application.Current!.Windows[0].Page!.DisplayAlert("Error", "Can't Delete the goal", "Ok");
            }
        }
    }
}
