using Homework16.Models.Employees;
using Homework16.ViewModels.Base;
using Homework16.Views.Pages;
using System.Windows.Input;
using System.Windows;
using System.Windows.Navigation;
using Homework16.Infrastructure.Commands;
using System.Collections.ObjectModel;
using Homework16.Models.Clients;
using Homework16.DataBase;
using Homework16.Services.Interfaces.DataBase;
using Homework16.Services.Runtime.DataBase;
using Homework16.Views.Dialogs;

namespace Homework16.ViewModels.Pages
{
    internal class MainPageViewModel : ViewModel
    {
        private Employee _employee;
        private NavigationService _navigationService;


        private ObservableCollection<Client> _clients = [];
        public ObservableCollection<Client> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }


        private ObservableCollection<Order> _orders = [];
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => Set(ref _orders, value);
        }


        private Client? _selectedClient;
        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _orders.Clear();
                _selectedOrder = null;
                Set(ref _selectedClient, value);
                GetOrders();
            }
        }


        private Order? _selectedOrder;
        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value);
        }

        #region Commands

        public ICommand AddClientCommand { get; }

        private bool CanAddClientCommandExecute(object p) => true;

        private void OnAddClientCommandExecuted(object p)
        {
            AddClientWindow addClientWindow = new();

            if(addClientWindow.ShowDialog() != null)
            {
                _selectedClient = null;
                _selectedOrder = null;
                _orders.Clear();
                _clients.Clear();
                Task.Run(_dataBaseService.GetAllClients);
            }
        }

        public ICommand DeleteClientCommand { get; }

        private bool CanDeleteClientCommandExecute(object p) => true;

        private void OnDeleteClientCommandExecuted(object p)
        {
            var result = MessageBox.Show("Удалить пользователя?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                _dataBaseService.DeleteClient(_selectedClient?.Id ?? -1);
                _selectedClient = null;
                _selectedOrder = null;
                _orders.Clear();
                _clients.Clear();
                Task.Run(_dataBaseService.GetAllClients);
            }
        }

        #endregion


        private IDataBaseService _dataBaseService = new DataBaseService();


        public MainPageViewModel(NavigationService navigationService, Employee employee)
        {
            _employee = employee;
            _navigationService = navigationService;

            #region Commands

            AddClientCommand = new RelayCommand(OnAddClientCommandExecuted, CanAddClientCommandExecute);
            DeleteClientCommand = new RelayCommand(OnDeleteClientCommandExecuted, CanDeleteClientCommandExecute);

            #endregion

            _dataBaseService.OnGetAllClients += c =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var client in c)
                    {
                        Clients.Add(client);
                    }
                });
            };

            _dataBaseService.OnGetOrders += o =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var order in o)
                    {
                        Orders.Add(order);
                    }
                });
            };

            Task.Run(_dataBaseService.GetAllClients);
        }

        private void GetOrders()
        {
            if(SelectedClient != null)
                Task.Run(async () => await _dataBaseService.GetOrders(SelectedClient.Email));
        }
    }
}
