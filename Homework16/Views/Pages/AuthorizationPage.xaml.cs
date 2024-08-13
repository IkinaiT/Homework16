using Homework16.ViewModels.Pages;
using Homework16.DataBase;
using System.Windows;
using System.Windows.Controls;

namespace Homework16.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();

            Loaded += AuthorizationPageLoaded;
        }

        private void AuthorizationPageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AuthorizationPageViewModel(NavigationService);
        }
    }
}
