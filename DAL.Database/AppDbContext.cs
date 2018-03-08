using Microsoft.EntityFrameworkCore;

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

        public DbSet<UserService.Domain.Models.User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connection);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
