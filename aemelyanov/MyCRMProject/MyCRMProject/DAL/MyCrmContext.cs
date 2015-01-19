using System.Data.Entity;
using MyCRMProject.Entities;

namespace MyCRMProject.DAL
{
    public class MyCrmContext : DbContext
    {
        public MyCrmContext()
            : base("MyCRMProject")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Operation> Operations { get; set; }
    }
}
