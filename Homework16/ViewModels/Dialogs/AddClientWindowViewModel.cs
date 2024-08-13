using Homework16.ViewModels.Base;
using Homework16.Views.Pages;
using Homework16.Views.Windows;
using System.Windows.Input;
using System.Windows;
using Homework16.Infrastructure.Commands;
using System.Windows.Navigation;
using System.Runtime.CompilerServices;
using Homework16.Services.Interfaces.DataBase;
using Homework16.Services.Runtime.DataBase;

namespace Homework16.ViewModels.Dialogs
{
    internal class AddClientWindowViewModel : ViewModel
    {
        #region Fields

        #region LastName

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                Set(ref _lastName, value);
                CheckNull();
            }
        }

        #endregion

        #region FirstName

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                Set(ref _firstName, value);
                CheckNull();
            }
        }

        #endregion

        #region MiddleName

        private string _middleName = string.Empty;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                Set(ref _middleName, value);
                CheckNull();
            }
        }

        #endregion

        #region Email

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                Set(ref _email, value);
                CheckNull();
            }
        }

        #endregion

        #region PhoneNumber

        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => Set(ref _phoneNumber, value);
        }

        #endregion

        #endregion

        #region Commands

        #region Accept

        public ICommand AcceptCommand { get; }

        private bool CanAcceptCommandExecute(object p) => true;

        private async void OnAcceptCommandExecuted(object p)
        {
            IDataBaseService db = new DataBaseService();
            try
            {
                var result = await db.AddClient(_lastName, _firstName, _middleName, _email, _phoneNumber);
                MessageBox.Show(result ? "Успешно" : "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion

        #region Properties

        #region AcceptButtonEnabled

        private bool _acceptButtonEnabled = false;
        public bool AcceptButtonEnabled
        {
            get => _acceptButtonEnabled;
            set => Set(ref _acceptButtonEnabled, value);
        }

        #endregion

        #endregion

        public AddClientWindowViewModel()
        {
            #region Commands

            //CancellCommand = new RelayCommand(OnCancellCommandExecuted, CanCancellCommandExecute);
            AcceptCommand = new RelayCommand(OnAcceptCommandExecuted, CanAcceptCommandExecute);

            #endregion
        }

        private void CheckNull()
        {
            AcceptButtonEnabled = !string.IsNullOrWhiteSpace(_lastName) &&
                !string.IsNullOrWhiteSpace(_firstName) &&
                !string.IsNullOrWhiteSpace(_middleName) &&
                !string.IsNullOrWhiteSpace(_email);
        }
    }
}
