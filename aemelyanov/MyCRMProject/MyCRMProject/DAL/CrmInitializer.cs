using System;
using System.Collections.Generic;
using MyCRMProject.Entities;

namespace MyCRMProject.DAL
{
    public class CrmInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<MyCrmContext>
    {
        protected override void Seed(MyCrmContext context)
        {
            var clients = new List<Client>
            {
                new Client {FirstName = "Manokhin", LastName = "Alexey", DateOfBirth = DateTime.Parse("26.05.1975")},
                new Client {FirstName = "Torkhanova", LastName = "Olga", DateOfBirth = DateTime.Parse("27.07.1988")},
                new Client {FirstName = "Gerasimov", LastName = "Sergey", DateOfBirth = DateTime.Parse("28.08.1990")}
            };
            clients.ForEach(s=> context.Clients.Add(s));
            context.SaveChanges();
        }
    }
}
