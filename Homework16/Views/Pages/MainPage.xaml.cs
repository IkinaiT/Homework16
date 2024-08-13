using Homework16.Models.Employees;
using Homework16.ViewModels.Pages;
using System.Windows;
using System.Windows.Controls;

namespace Homework16.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Employee _employee;

        public MainPage(Employee e)
        {
            _employee = e;

            InitializeComponent();

            Loaded += MainPageLoaded;
        }

        private void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainPageViewModel(NavigationService, _employee);
        }
    }
}
