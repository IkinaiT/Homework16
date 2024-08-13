using Homework16.Models.Clients;
using Npgsql;

namespace Homework16.DataBase
{
    public class PostgressSqlDbContext
    {
        private NpgsqlConnectionStringBuilder _connectionString = new() 
        {
            Host = "localhost",
            Username = "postgres",
            Password = "admin",
            Database = "clients",
            Pooling = true
        };

        public async Task<List<Client>> GetAllClients()
        {
            List<Client> result = [];

            try
            {
                var sqlExpression = "SELECT * FROM public.\"Clients\"";

                using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString.ConnectionString))
                {
                    await connection.OpenAsync();

                    NpgsqlCommand command = new NpgsqlCommand(sqlExpression, connection);

                    var reader = command.ExecuteReader();

                    while(await reader.ReadAsync())
                    {
                        result.Add(new()
                        {
                            Id = (int)reader["Id"],
                            LastName = (string)reader["LastName"],
                            FirstName = (string)reader["FirstName"],
                            MiddleName = (string)reader["MiddleName"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string)reader["PhoneNumber"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<List<Order>> GetOrders(string email)
        {
            List<Order> result = [];



            return result;
        }
    }
}
