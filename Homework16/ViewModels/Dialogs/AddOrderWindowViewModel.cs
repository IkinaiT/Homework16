using Homework16.Services.Interfaces.DataBase;
using Homework16.Services.Runtime.DataBase;
using Homework16.ViewModels.Base;
using System.Windows.Input;
using System.Windows;
using Homework16.Infrastructure.Commands;

namespace Homework16.ViewModels.Dialogs
{
    internal class AddOrderWindowViewModel : ViewModel
    {
        private string _email = string.Empty;

        #region Fields

        private string _code = string.Empty;
        public string Code
        {
            get => _code;
            set
            {
                CheckNull();
                Set(ref _code, value);
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                CheckNull();
                Set(ref _name, value);
            }
        }

        #endregion

        #region Commands

        public ICommand AcceptCommand { get; }

        private bool CanAcceptCommandExecute(object p) => true;

        private async void OnAcceptCommandExecuted(object p)
        {
            IDataBaseService db = new DataBaseService();
            try
            {
                var result = await db.AddOrder(_email, int.Parse(_code), _name);
                MessageBox.Show(result ? "Успешно" : "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Properties

        private bool _acceptButtonEnabled = false;
        public bool AcceptButtonEnabled
        {
            get => _acceptButtonEnabled;
            set => Set(ref _acceptButtonEnabled, value);
        }

        #endregion


        public AddOrderWindowViewModel(string email)//добавить передачу мыла
        {
            _email = email;
            #region Commands

            AcceptCommand = new RelayCommand(OnAcceptCommandExecuted, CanAcceptCommandExecute);

            #endregion
        }

        private void CheckNull()
        {
            AcceptButtonEnabled = !string.IsNullOrWhiteSpace(Code) && !string.IsNullOrWhiteSpace(Name) && int.TryParse(_code, out var temp);
        }
    }
}
