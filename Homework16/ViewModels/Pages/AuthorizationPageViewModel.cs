using Homework16.Infrastructure.Commands;
using Homework16.ViewModels.Base;
using Homework16.Views.Pages;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Homework16.ViewModels.Pages
{
    internal class AuthorizationPageViewModel : ViewModel
    {
        #region Text fields

        #region Login input

        private string _loginInput = string.Empty;
        public string LoginInput
        {
            get => _loginInput;
            set
            {
                Set(ref _loginInput, value);
            }
        }

        #endregion
        #region Password input

        private string _passwordInput = string.Empty;
        public string PasswordInput
        {
            get => _passwordInput;
            set
            {
                Set(ref _passwordInput, value);
            }
        }

        #endregion

        #endregion

        #region Button fields

        #region Authorization button

        private string _buttonText = "Войти";
        public string ButtonText
        {
            get => _buttonText;
            set
            {
                Set(ref _buttonText, value);
            }
        }

        #endregion

        #endregion

        #region Commands

        #region AuthorizationCommand

        public ICommand AuthorizationCommand { get; }

        private bool CanAuthorizationCommandExecute(object p) => true;

        private void OnAuthorizationCommandExecuted(object p)
        {
            _navigationService.Navigate(new MainPage());

        }

        #endregion

        #endregion

        private NavigationService _navigationService;

        public AuthorizationPageViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            #region Commands

            AuthorizationCommand = new RelayCommand(OnAuthorizationCommandExecuted, CanAuthorizationCommandExecute);

            #endregion
        }
    }
}
