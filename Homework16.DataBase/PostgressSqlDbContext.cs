using Homework16.Models.Clients;
using Homework16.Models.Employees;
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
                        var t = reader.IsDBNull(4) ? null : reader.GetValue(4);

                        result.Add(new()
                        {
                            Id = (int)reader["Id"],
                            LastName = (string)reader["LastName"],
                            FirstName = (string)reader["FirstName"],
                            MiddleName = (string)reader["MiddleName"],
                            Email = (string)reader["Email"],
                            PhoneNumber = (string?)t
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

            try
            {
                var sqlExpression = $"SELECT * FROM public.\"Orders\" WHERE \"Orders\".\"ClientEmail\" = '{email.ToLower()}' ORDER BY \"Id\"";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.ConnectionString))
                {
                    await connection.OpenAsync();

                    NpgsqlCommand command = new NpgsqlCommand(sqlExpression, connection);

                    var reader = command.ExecuteReader();

                    while (await reader.ReadAsync())
                    {
                        result.Add(new()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Code = (int)reader["Code"],
                            ClientEmail = (string)reader["ClientEmail"]
                        });
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public async Task<bool> AddClient(string lastName, string firstMane, string middleName, string email, string phoneNumber)
        {
            try
            {
                var sqlExpression = $"INSERT INTO public.\"Clients\" (\r\n\"LastName\", \"FirstName\", \"MiddleName\", \"Email\", \"PhoneNumber\") " +
                    $"VALUES (\r\n'{lastName}'::text, '{firstMane}'::text, '{middleName}'::text, '{email}'::text, '{phoneNumber}'::text)";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.ConnectionString))
                {
                    await connection.OpenAsync();

                    NpgsqlCommand command = new NpgsqlCommand(sqlExpression, connection);

                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public async Task<bool> DeleteClient(int id)
        {
            try
            {
                var sqlExpression = $"DELETE FROM public.\"Clients\"\r\n\tWHERE \"Clients\".\"Id\" = {id}";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.ConnectionString))
                {
                    await connection.OpenAsync();

                    NpgsqlCommand command = new NpgsqlCommand(sqlExpression, connection);

                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public async Task<bool> AddOrder(string email, int code, string name)
        {
            try
            {
                var sqlExpression = $"INSERT INTO public.\"Orders\" (\r\n\"ClientEmail\", \"FirstName\", \"MiddleName\") " +
                    $"VALUES (\r\n'{email}'::text, '{code}'::integer, '{name}'::text)";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.ConnectionString))
                {
                    await connection.OpenAsync();

                    NpgsqlCommand command = new NpgsqlCommand(sqlExpression, connection);

                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                var sqlExpression = $"DELETE FROM public.\"Clients\"\r\n\tWHERE \"Clients\".\"Id\" = {id}";

                using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString.ConnectionString))
                {
                    await connection.OpenAsync();

                    NpgsqlCommand command = new NpgsqlCommand(sqlExpression, connection);

                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {

            return false;
        }
    }
}
