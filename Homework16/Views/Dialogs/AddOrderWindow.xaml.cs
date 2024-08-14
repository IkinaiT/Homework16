using Homework16.ViewModels.Dialogs;
using System.Windows;

namespace Homework16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        private string _email = string.Empty;

        public AddOrderWindow(string email)
        {
            InitializeComponent();

            Loaded += AuthorizationPageLoaded;
            _email = email;
        }

        private void AuthorizationPageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AddOrderWindowViewModel(_email);
        }
    }
}
