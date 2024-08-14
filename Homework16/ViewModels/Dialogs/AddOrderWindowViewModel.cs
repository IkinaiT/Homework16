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
            set => Set(ref _code, value);
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
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


        public AddOrderWindowViewModel(string email)//добавить передачу мыла
        {
            _email = email;
            #region Commands

            AcceptCommand = new RelayCommand(OnAcceptCommandExecuted, CanAcceptCommandExecute);

            #endregion
        }
    }
}
