using Homework16.DataBase;
using Homework16.DataBase.EntityFramework;
using Homework16.Models.Clients;
using Homework16.Models.Employees;
using Homework16.Services.Interfaces.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace Homework16.Services.Runtime.DataBase
{
    public class DataBaseService : IDataBaseService
    {
        #region ADO .NET

        //private readonly MSSQLDbContext _employeesDb = new();
        //private readonly PostgressSqlDbContext _clientsDb = new();
        //public Action? OnInit { get; set; }
        //public Action<List<Client>>? OnGetAllClients { get; set; }
        //public Action<List<Order>>? OnGetOrders { get; set; }

        //public async Task Init()
        //{
        //    await _employeesDb.Init();
        //    OnInit?.Invoke();
        //}
        //public async Task<Employee?> Authorization(string email, string password)
        //{
        //    var result = await _employeesDb.Authorization(email, password);

        //    if (result is Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);

        //        return null;
        //    }

        //    return result is Employee obj ? obj : null;
        //}

        //public async Task GetAllClients()
        //{
        //    var result = await _clientsDb.GetAllClients();

        //    OnGetAllClients?.Invoke(result);
        //}

        //public async Task GetOrders(string email)
        //{
        //    var result = await _clientsDb.GetOrders(email);

        //    OnGetOrders?.Invoke(result);
        //}

        //public async Task<bool> AddClient(string lastName, string firstMane, string middleName, string email, string phoneNumber)
        //{
        //    return await _clientsDb.AddClient(lastName, firstMane, middleName, email, phoneNumber);
        //}

        //public async Task<bool> DeleteClient(int id)
        //{
        //    return await _clientsDb.DeleteClient(id);
        //}

        //public async Task<bool> AddOrder(string email, int code, string name)
        //{
        //    return await _clientsDb.AddOrder(email, code, name);
        //}

        //public async Task<bool> DeleteOrder(int id)
        //{
        //    return await _clientsDb.DeleteOrder(id);
        //}

        //public async Task<bool> UpdateEmployee(Employee employee)
        //{
        //    return await _employeesDb.UpdateEmployee(employee);
        //}

        #endregion

        #region EntityFramework

        private EmployeesContext _employeesContext;
        private ClientsContext _clientsContext;

        public Action? OnInit { get; set; }
        public Action<List<Client>>? OnGetAllClients { get; set; }
        public Action<List<Order>>? OnGetOrders { get; set; }


        public DataBaseService()
        {
            var optionsMSSQL = new DbContextOptionsBuilder<EmployeesContext>();

            optionsMSSQL.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=employees;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            _employeesContext = new(optionsMSSQL.Options);

            var optionsPostgresSQL = new DbContextOptionsBuilder<ClientsContext>();

            optionsPostgresSQL.UseNpgsql("Host=localhost;Username=postgres;Password=admin;Database=clients;Pooling=True");

            _clientsContext = new(optionsPostgresSQL.Options);
        }

        public async Task Init()
        {
            OnInit?.Invoke();
        }

        public async Task<bool> AddClient(string lastName, string firstMane, string middleName, string email, string phoneNumber)
        {
            try
            {
                _clientsContext.Clients.Add(new()
                {
                    Email = email,
                    FirstName = firstMane,
                    MiddleName = middleName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                });

                await _clientsContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) { }

            return false;
        }

        public async Task<bool> AddOrder(string email, int code, string name)
        {
            try
            {
                _clientsContext.Orders.Add(new()
                {
                    Name = name,
                    Code = code,
                    ClientEmail = email,
                });

                await _clientsContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) { }

            return false;
        }

        public async Task<Employee?> Authorization(string email, string password)
        {
            try
            {
                var user = await _employeesContext.Employees.Where(_ => _.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.Password == password) 
                        return user;
                    else 
                        throw new Exception("Пароли не совпадают");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new()
                {
                    Id = -1
                };
            }

            return null;
        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                var clientToRemove = await _clientsContext.Clients.Where(_ => _.Id == id).FirstOrDefaultAsync();

                if (clientToRemove == null)
                    throw new Exception("Клиент не найден");

                _clientsContext.Clients.Remove(clientToRemove);

                await _clientsContext.SaveChangesAsync();

                await GetAllClients();

                return true;
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                var orderToRemove = await _clientsContext.Orders.Where(_ => _.Id == id).FirstOrDefaultAsync();

                if (orderToRemove == null)
                    throw new Exception("Клиент не найден");

                _clientsContext.Orders.Remove(orderToRemove);

                await _clientsContext.SaveChangesAsync();

                await GetOrders(orderToRemove.ClientEmail);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        public async Task GetAllClients()
        {
            try
            {
                var result = _clientsContext.Clients.ToList();

                OnGetAllClients?.Invoke(result);
            }
            catch (Exception ex) { }
        }

        public async Task GetOrders(string email)
        {
            try
            {
                var result = _clientsContext.Orders.ToList();

                OnGetOrders?.Invoke(result);
            }
            catch (Exception ex) { }
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            try
            {
                var user = await _employeesContext.Employees.Where(_ => _.Id == employee.Id).FirstOrDefaultAsync();
                
                if(user != null)
                {
                    user.FirstName = employee.FirstName;
                    user.LastName = employee.LastName;
                    user.MiddleName = employee.MiddleName;
                    user.Age = employee.Age;

                    _employeesContext.Update(user);
                }


                await _employeesContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex) { }

            return false;
        }

        #endregion
    }
}
