using Homework16.Models.Employees;
using System;
using System.Data.SqlClient;

namespace Homework16.DataBase
{
    public class MSSQLDbContext
    {
        private SqlConnectionStringBuilder _stringConnectionBase = new()
        {
           DataSource = @"(localdb)\MSSQLLocalDB",
           InitialCatalog = "master",
           IntegratedSecurity = true,
           Pooling = true,
        };
        private SqlConnectionStringBuilder _stringConnection = new()
        {
            DataSource = @"(localdb)\MSSQLLocalDB",
            InitialCatalog = "employees",
            IntegratedSecurity = true,
            Pooling = true,
        };


        public async Task Init()
        {
            try
            {
                using(SqlConnection _connection = new(_stringConnection.ConnectionString))
                {
                    _connection.Open();
                }
            }
            catch
            {
                await GenerateDb();
            }
        }

        private async Task GenerateDb()
        {
            try
            {
                using(SqlConnection con = new(_stringConnectionBase.ConnectionString))
                {
                    await con.OpenAsync();

                    SqlCommand command = new()
                    {
                        Connection = con,
                        CommandText = "CREATE DATABASE employees"
                    };
                    await command.ExecuteNonQueryAsync();

                    await con.CloseAsync();
                }

                await CreateBosses();

                await CreateEmployees();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            
        }

        private async Task CreateBosses()
        {
            using (SqlConnection _connection = new(_stringConnection.ConnectionString))
            {
                await _connection.OpenAsync();

                SqlCommand command = new()
                {
                    Connection = _connection,

                    CommandText = @"CREATE TABLE Bosses (Id INT PRIMARY KEY IDENTITY, 
                                    LastName NVARCHAR(100) NOT NULL,
                                    FirstName NVARCHAR(100) NOT NULL,
                                    MiddleName NVARCHAR(100) NOT NULL,
                                    Age INT NOT NULL, 
                                    Email NVARCHAR(100) NOT NULL
                                    )"
                };
                await command.ExecuteNonQueryAsync();


                command.CommandText = $"INSERT INTO Bosses (LastName, FirstName, MiddleName, Age, Email) " +
                    $"VALUES ('Ivanov', 'Ivan', 'Ivanovich', 45, 'ivanovii@mail.ru')";
                await command.ExecuteNonQueryAsync();


                command.CommandText = $"INSERT INTO Bosses (LastName, FirstName, MiddleName, Age, Email) " +
                    $"VALUES ('Petrov', 'Petr', 'Petrovich', 38, 'petrovpp@yandex.ru')";
                await command.ExecuteNonQueryAsync();

                await _connection.CloseAsync();
            }
        }

        private async Task CreateEmployees()
        {
            using (SqlConnection _connection = new(_stringConnection.ConnectionString))
            {
                await _connection.OpenAsync();

                SqlCommand command = new()
                {
                    Connection = _connection,

                    CommandText = @"CREATE TABLE Employees (Id INT PRIMARY KEY IDENTITY, 
                                    LastName NVARCHAR(100) NOT NULL,
                                    FirstName NVARCHAR(100) NOT NULL,
                                    MiddleName NVARCHAR(100) NOT NULL,
                                    Age INT NOT NULL, 
                                    Email NVARCHAR(100) NOT NULL,
                                    Password NVARCHAR(100) NOT NULL,
                                    BossId INT NOT NULL,
                                    FOREIGN KEY (BossId) REFERENCES Bosses (Id)
                                    )"
                };
                await command.ExecuteNonQueryAsync();


                command.CommandText = $"INSERT INTO Employees (LastName, FirstName, MiddleName, Age, Email, Password, BossId) " +
                    $"VALUES ('Andreev', 'Andrey', 'Andreevich', 23, 'andreevaa@mail.ru', '123123213', 1)";
                await command.ExecuteNonQueryAsync();

                command.CommandText = $"INSERT INTO Employees (LastName, FirstName, MiddleName, Age, Email, Password, BossId) " +
                    $"VALUES ('Ilyin', 'Ilya', 'Ilyich', 28, 'ilyinii@mail.ru', 'qweqweqwe', 1)";
                await command.ExecuteNonQueryAsync();

                command.CommandText = $"INSERT INTO Employees (LastName, FirstName, MiddleName, Age, Email, Password, BossId) " +
                    $"VALUES ('Yurjev', 'Yuriy', 'Yurievich', 31, 'yurjevyy@mail.ru', 'q1w2e3r4', 2)";
                await command.ExecuteNonQueryAsync();

                command.CommandText = $"INSERT INTO Employees (LastName, FirstName, MiddleName, Age, Email, Password, BossId) " +
                    $"VALUES ('Volodin', 'Vladimir', 'Vladimirovich', 62, 'volodinvv@mail.ru', 'qwerty123456', 2)";
                await command.ExecuteNonQueryAsync();

                await _connection.CloseAsync();
            }
        }

        public async Task<object?> Authorization(string email, string password)
        {
            try
            {
                string sqlExpression = $"SELECT * FROM Employees WHERE Email = '{email.ToLower()}' AND Password = '{password}'";

                using (SqlConnection _connection = new(_stringConnection.ConnectionString))
                {
                    await _connection.OpenAsync();

                    SqlCommand command = new(sqlExpression, _connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if(await reader.ReadAsync())
                    {
                        return new Employee
                        {
                            Id = (int)reader["Id"],
                            LastName = (string)reader["LastName"],
                            FirstName = (string)reader["FirstName"],
                            MiddleName = (string)reader["MiddleName"],
                            Age = (int)reader["Age"],
                            Email = (string)reader["Email"],
                            BossId = (int)reader["BossId"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }

        public async Task<Boss?> GetBoss(int id)
        {


            return null;
        }
    }
}
