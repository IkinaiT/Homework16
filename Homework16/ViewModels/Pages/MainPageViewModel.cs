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
            set
            {
                Set(ref _clients, value);
            }
        }


        private ObservableCollection<Order> _orders = [];
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                Set(ref _orders, value);
            }
        }


        private Client? _selectedClient;
        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _orders.Clear();
                GetOrders();
                Set(ref _selectedClient, value);
            }
        }


        private IDataBaseService _dataBaseService = new DataBaseService();


        public MainPageViewModel(NavigationService navigationService, Employee employee)
        {
            _employee = employee;
            _navigationService = navigationService;

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
