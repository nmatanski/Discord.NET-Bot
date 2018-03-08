using DAL.Database.Helpers.InMemoryObjects;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database.Helpers
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.Migrate();

            InitialUsers.Seed(context);
        }
    }
}
