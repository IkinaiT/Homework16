using Homework16.ViewModels.Dialogs;
using System.Windows;

namespace Homework16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для UpdateEmployeeWindow.xaml
    /// </summary>
    public partial class UpdateEmployeeWindow : Window
    {
        public UpdateEmployeeWindow()
        {
            InitializeComponent();

            Loaded += AuthorizationPageLoaded;
        }

        private void AuthorizationPageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new UpdateEmployeeWindowViewModel();
        }
    }
}
