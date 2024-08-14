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

        #region Collections

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

        #endregion

        #region Selected

        private Client? _selectedClient;
        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _orders.Clear();
                SelectedOrder = null;

                DeleteOrderButtonEnabled = false;
                AddOrderButtonEnabled = value != null;

                Set(ref _selectedClient, value);
                GetOrders();
            }
        }


        private Order? _selectedOrder;
        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set 
            {
                DeleteOrderButtonEnabled = value != null;
                Set(ref _selectedOrder, value);
            } 
        }

        #endregion

        #region Properties

        private bool _addOrderButtonEnabled;
        public bool AddOrderButtonEnabled
        {
            get => _addOrderButtonEnabled;
            set => Set(ref _addOrderButtonEnabled, value);
        }

        private bool _deleteOrderButtonEnabled;
        public bool DeleteOrderButtonEnabled
        {
            get => _deleteOrderButtonEnabled;
            set => Set(ref _deleteOrderButtonEnabled, value);
        }

        #endregion

        #region Commands

        public ICommand AddClientCommand { get; }

        private bool CanAddClientCommandExecute(object p) => true;

        private void OnAddClientCommandExecuted(object p)
        {
            AddClientWindow addClientWindow = new();

            if(addClientWindow.ShowDialog() != null)
            {
                SelectedClient = null;
                SelectedOrder = null;
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
                SelectedClient = null;
                SelectedOrder = null;
                _orders.Clear();
                _clients.Clear();
                Task.Run(_dataBaseService.GetAllClients);
            }
        }

        public ICommand AddOrderCommand { get; }

        private bool CanAddOrderCommandExecute(object p) => true;

        private void OnAddOrderCommandExecuted(object p)
        {
            
        }

        public ICommand DeleteOrderCommand { get; }

        private bool CanDeleteOrderCommandExecute(object p) => true;

        private void OnDeleteOrderCommandExecuted(object p)
        {
            var result = MessageBox.Show("Удалить заказ?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(result == MessageBoxResult.Yes)
            {
                //_dataBaseService.DeleteClient(_selectedClient?.Id ?? -1);
                //_selectedClient = null;
                //_selectedOrder = null;
                //_orders.Clear();
                //_clients.Clear();
                //Task.Run(_dataBaseService.GetAllClients);
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
            AddOrderCommand = new RelayCommand(OnAddClientCommandExecuted, CanAddClientCommandExecute);
            DeleteOrderCommand = new RelayCommand(OnDeleteClientCommandExecuted, CanDeleteClientCommandExecute);

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
