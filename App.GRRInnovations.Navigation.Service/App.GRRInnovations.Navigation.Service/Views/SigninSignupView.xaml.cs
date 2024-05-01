using App.GRRInnovations.Navigation.Service.ViewModels;

namespace App.GRRInnovations.Navigation.Service.Views;

public partial class SigninSignupView : ContentPage
{
	public SigninSignupView(SigninSignupViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}