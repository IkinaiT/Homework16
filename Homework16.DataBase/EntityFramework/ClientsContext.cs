using Homework16.Models.Clients;
using Microsoft.EntityFrameworkCore;

namespace Homework16.DataBase.EntityFramework
{
    public class ClientsContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ClientsContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
