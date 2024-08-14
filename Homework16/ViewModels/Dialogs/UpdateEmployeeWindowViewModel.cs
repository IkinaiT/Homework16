using Homework16.Models.Employees;
using Homework16.Services.Interfaces.DataBase;
using Homework16.Services.Runtime.DataBase;
using Homework16.ViewModels.Base;
using System.Windows.Input;
using System.Windows;
using Homework16.Infrastructure.Commands;

namespace Homework16.ViewModels.Dialogs
{
    internal class UpdateEmployeeWindowViewModel : ViewModel
    {
        private Employee _employee;

        #region Fields

        #region LastName

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }

        #endregion

        #region FirstName

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }

        #endregion

        #region MiddleName

        private string _middleName = string.Empty;
        public string MiddleName
        {
            get => _middleName;
            set => Set(ref _middleName, value);
        }

        #endregion

        #region Email

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        #endregion

        #region Age

        private string _age = string.Empty;
        public string Age
        {
            get => _age;
            set => Set(ref _age, value);
        }

        #endregion

        #endregion

        #region Commands

        #region Accept

        public ICommand AcceptCommand { get; }

        private bool CanAcceptCommandExecute(object p) => true;

        private async void OnAcceptCommandExecuted(object p)
        {
            var correct = int.TryParse(_age, out var intAge);
            if (!correct)
            {
                MessageBox.Show("Ошибка! Возраст должен быть числом!");
            }

            IDataBaseService db = new DataBaseService();
            try
            {
                _employee.FirstName = _firstName;
                _employee.MiddleName = _middleName;
                _employee.Email = _email;
                _employee.Age = intAge;
                _employee.LastName = _lastName;

                var result = await db.UpdateEmployee(_employee);
                MessageBox.Show(result ? "Успешно" : "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion

        public UpdateEmployeeWindowViewModel(Employee employee)
        {
            _employee = employee;

            #region Commands

            AcceptCommand = new RelayCommand(OnAcceptCommandExecuted, CanAcceptCommandExecute);

            #endregion

            LastName = _employee.LastName;
            FirstName = _employee.FirstName;
            MiddleName = _employee.MiddleName;
            Email = _employee.Email;
            Age = _employee.Age.ToString();

        }
    }
}
