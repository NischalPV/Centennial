using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Centennial.Api.Data
{
    public class MasterData
    {
        public static void SeedUsingMigration(ModelBuilder modelBuilder)
        {
            DefaultStatuses(modelBuilder);
            DefaultTransactionTypes(modelBuilder);
            DefaultMaterials(modelBuilder);
        }

        private static void DefaultStatuses(ModelBuilder modelBuilder)
        {
            List<Entities.Status> StatusData = new List<Entities.Status>();
            foreach(var statusId in Enum.GetValues(typeof(Entities.Constants.StatusIds)))
            {
                StatusData.Add(new Entities.Status()
                {
                    Id = (int)statusId,
                    Name = Enum.GetName(typeof(Entities.Constants.StatusIds), statusId),
                    IsActive = true
                });
            }

            foreach(var status in StatusData)
            {
                modelBuilder.Entity<Entities.Status>().HasData(status);
            }
        }

        private static void DefaultTransactionTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.TransactionType>().HasData(new Entities.TransactionType { Id = 1, Name = "Sent to" });
            modelBuilder.Entity<Entities.TransactionType>().HasData(new Entities.TransactionType { Id = 2, Name = "Received from" });
            modelBuilder.Entity<Entities.TransactionType>().HasData(new Entities.TransactionType { Id = 3, Name = "Added by Stock record" });
            modelBuilder.Entity<Entities.TransactionType>().HasData(new Entities.TransactionType { Id = 4, Name = "Sent to customer" });
        }

        private static void DefaultMaterials(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Material>().HasData(new Entities.Material(id: "410b785b-5484-48f5-8555-7974fe5b63dd", name: "SS", createdDate: new DateTime(2021, 7, 13, 18, 48, 12, 823, DateTimeKind.Utc).AddTicks(7840)));
            modelBuilder.Entity<Entities.Material>().HasData(new Entities.Material(id: "b0217cf5-af55-4472-bf79-27167df5ee52", name: "Titanium", createdDate: new DateTime(2021, 7, 13, 18, 48, 12, 823, DateTimeKind.Utc).AddTicks(9080)));
        }
    }
}
