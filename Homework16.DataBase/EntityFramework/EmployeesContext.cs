using Homework16.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace Homework16.DataBase.EntityFramework
{
    public class EmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Boss> Bosses { get; set; }

        public EmployeesContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
