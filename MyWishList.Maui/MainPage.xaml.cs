using MyWishList.Maui.ViewModels;

namespace MyWishList.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}
