using System.Data.SqlClient;

namespace Homework16.DataBase
{
    public class MSSQLDbContext
    {
        private SqlConnectionStringBuilder _stringConnection = new SqlConnectionStringBuilder()
        {
            DataSource = @"(localdb)\MSSQLLocalDB",
            InitialCatalog = "EmployeesDb",
            IntegratedSecurity = true,
            Pooling = true
        };
        private SqlConnection _connection;

        public MSSQLDbContext()
        {
            _connection = new() { ConnectionString = _stringConnection.ConnectionString };
        }

        public async Task Init()
        {
            try
            {
                _connection.Open();
            }
            catch
            {
                _stringConnection = new SqlConnectionStringBuilder()
                {
                    DataSource = @"(localdb)\MSSQLLocalDB",
                    IntegratedSecurity = true,
                    Pooling = true
                };

                _connection = new() { ConnectionString = _stringConnection.ConnectionString };

                _connection.Open();

                await GenerateDb();
            }
        }

        public void Close() => _connection.Close();

        private async Task GenerateDb()
        {
            SqlCommand command = new();
            try
            {
                command = new()
                {
                    Connection = _connection,
                    CommandText = "CREATE DATABASE EmployeesDb"
                };
                await command.ExecuteNonQueryAsync();

                command.CommandText = @"CREATE TABLE Bosses (Id INT PRIMARY KEY IDENTITY, 
                                    LastName NVARCHAR(100) NOT NULL,
                                    FirstName NVARCHAR(100) NOT NULL,
                                    MiddleName NVARCHAR(100) NOT NULL,
                                    Age INT NOT NULL, 
                                    Email NVARCHAR(100) NOT NULL
                                    )";
                await command.ExecuteNonQueryAsync();

                for (int i = 0; i < 10; i++)
                {
                    command.CommandText = $"INSERT INTO Bosses (LastName, FirstName, MiddleName, Age, Email) VALUES (LastName{i}, FirstName{i}, MiddleName{i}, {20 + i}, Email{i})";
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {

            }
            

            
        }
    }
}
