using App.GRRInnovations.Navigation.Service.ViewModels;

namespace App.GRRInnovations.Navigation.Service.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}