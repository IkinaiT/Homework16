using Homework16.ViewModels.Base;
using Homework16.Views.Pages;
using System.Windows.Controls;

namespace Homework16.ViewModels.Windows
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _title = string.Empty;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private Frame _frame = new Frame();
        public Frame Frame
        {
            get => _frame;
            set => Set(ref _frame, value);
        }

        public MainWindowViewModel()
        {
            Frame.Content = new AuthorizationPage();
        }
    }
}
