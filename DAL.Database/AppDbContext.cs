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

        //public DbSet<ITGigs.UserService.Domain.Models.User> Users { get; set; }

        //public DbSet<ITGigs.VenueService.Domain.Models.Venue> Venues { get; set; }

        //public DbSet<ITGigs.ITGigService.Domain.Models.ITGig> ITGigs { get; set; }

        //public DbSet<ITGigs.LogService.Domain.Models.CustomException> CustomExceptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
