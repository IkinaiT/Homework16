using Homework16.DataBase;
using Homework16.Models.Clients;
using Homework16.Models.Employees;
using Homework16.Services.Interfaces.DataBase;
using System.Windows;

namespace Homework16.Services.Runtime.DataBase
{
    public class DataBaseService : IDataBaseService
    {
        private readonly MSSQLDbContext _employeesDb = new();
        private readonly PostgressSqlDbContext _clientsDb = new();
        public Action? OnInit { get; set; }
        public Action<List<Client>>? OnGetAllClients { get; set; }
        public Action<List<Order>>? OnGetOrders { get; set; }

        public async Task Init()
        {
            await _employeesDb.Init();
            OnInit?.Invoke();
        }
        public async Task<Employee?> Authorization(string email, string password)
        {
            var result = await _employeesDb.Authorization(email, password);

            if(result is Exception ex)
            {
                MessageBox.Show(ex.Message);

                return null;
            }

            return result is Employee obj ? obj : null;
        }

        public async Task GetAllClients()
        {
            var result = await _clientsDb.GetAllClients();

            OnGetAllClients?.Invoke(result);
        }

        public async Task GetOrders(string email)
        {
            var result = await _clientsDb.GetOrders(email);

            OnGetOrders?.Invoke(result);
        }

        public async Task<bool> AddClient(string lastName, string firstMane, string middleName, string email, string phoneNumber)
        {
            return await _clientsDb.AddClient(lastName, firstMane, middleName, email, phoneNumber);
        }

        public async Task<bool> DeleteClient(int id)
        {
            return await _clientsDb.DeleteClient(id);
        }

        public async Task<bool> AddOrder(string email, int code, string name)
        {
            return await _clientsDb.AddOrder(email, code, name);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            return await _clientsDb.DeleteOrder(id);
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            return await _employeesDb.UpdateEmployee(employee);
        }
    }
}
