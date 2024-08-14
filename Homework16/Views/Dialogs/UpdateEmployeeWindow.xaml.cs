using Homework16.Models.Employees;
using Homework16.ViewModels.Dialogs;
using System.Windows;

namespace Homework16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для UpdateEmployeeWindow.xaml
    /// </summary>
    public partial class UpdateEmployeeWindow : Window
    {
        private Employee _employee;

        public UpdateEmployeeWindow(Employee employee)
        {
            InitializeComponent();

            _employee = employee;
            Loaded += AuthorizationPageLoaded;
        }

        private void AuthorizationPageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new UpdateEmployeeWindowViewModel(_employee);
        }
    }
}
