using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Database
{
    public class AppDbContext : DbContext
    {
        private string connection;

        public AppDbContext()
        {
            connection = DBConnections.GetAppHarborConnection();
            //connection = DBConnections.GetAzureConnection();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
