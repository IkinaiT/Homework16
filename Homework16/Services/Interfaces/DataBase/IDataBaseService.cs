﻿using Homework16.Models.Clients;
using Homework16.Models.Employees;

namespace Homework16.Services.Interfaces.DataBase
{
    public interface IDataBaseService
    {
        public Action? OnInit { get; set; }
        public Action<List<Client>>? OnGetAllClients { get; set; }
        public Action<List<Order>>? OnGetOrders { get; set; }

        public Task Init();
        public Task<Employee?> Authorization(string email, string password);

        public Task GetAllClients(); 
        public Task GetOrders(string email);

        public Task<bool> AddClient(string lastName, string firstName, string middleName, string email, string phoneNumber);
        public Task<bool> DeleteClient(int id);
        
        public Task<bool> AddOrder(string email, int code, string name);
        public Task<bool> DeleteOrder(int id);

        public Task<bool> UpdateEmployee(Employee employee);
    }
}
