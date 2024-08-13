using Homework16.ViewModels.Dialogs;
using Homework16.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework16.Views.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();

            Loaded += AuthorizationPageLoaded;
        }

        private void AuthorizationPageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AddClientWindowViewModel();
        }
    }
}
