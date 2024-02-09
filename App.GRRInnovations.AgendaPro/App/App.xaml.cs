using App.GRRInnovations.AgendaPro.Views;

namespace App.GRRInnovations.AgendaPro
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            

            MainPage = new MenuTabbedPageView();
        }
    }
}