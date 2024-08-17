using Homework16.Infrastructure.Commands;
using Homework16.Services.Interfaces.DataBase;
using Homework16.Services.Runtime.DataBase;
using Homework16.ViewModels.Base;
using Homework16.Views.Pages;
using Homework16.Views.Windows;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Homework16.ViewModels.Pages
{
    internal class AuthorizationPageViewModel : ViewModel
    {
        #region Text fields

        #region Login input

        private string _loginInput = "andreevaa@mail.ru";
        public string LoginInput
        {
            get => _loginInput;
            set => Set(ref _loginInput, value);
        }

        #endregion
        #region Password input

        private string _passwordInput = "123123213";
        public string PasswordInput
        {
            get => _passwordInput;
            set => Set(ref _passwordInput, value);
        }

        #endregion

        #endregion

        #region Progressbar

        private string _progressbarVisibility = "Visible";
        public string ProgressbarVisibility
        {
            get => _progressbarVisibility;
            set
            {
                Set(ref _progressbarVisibility, value);
            }
        }

        #endregion

        #region Button

        private string _buttonText = "Войти";
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                Set(ref _buttonText, value);
            }
        }

        private bool _buttonEnabled = false;
        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set
            {
                Set(ref _buttonEnabled, value);
            }
        }


        #endregion

        #region Commands

        #region AuthorizationCommand

        public ICommand AuthorizationCommand { get; }

        private bool CanAuthorizationCommandExecute(object p) => true;

        private async void OnAuthorizationCommandExecuted(object p)
        {
            var result = await _dataBaseService.Authorization(_loginInput, _passwordInput);


            if (result != null)
            {
                if(result.Id != -1)
                    _navigationService.Navigate(new MainPage(result));
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
                
        }

        #endregion

        #endregion

        private NavigationService _navigationService;
        private IDataBaseService _dataBaseService = new DataBaseService();

        public AuthorizationPageViewModel(NavigationService navigationService)
        {
            _dataBaseService.OnInit += () => ButtonEnabled = true;
            _dataBaseService.OnInit += () => ProgressbarVisibility = "Hidden";
            _navigationService = navigationService;
            Task.Run(_dataBaseService.Init);

            #region Commands

            AuthorizationCommand = new RelayCommand(OnAuthorizationCommandExecuted, CanAuthorizationCommandExecute);

            #endregion
        }
    }
}
