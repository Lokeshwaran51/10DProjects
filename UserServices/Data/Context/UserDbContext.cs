using Microsoft.EntityFrameworkCore;

namespace UserServices.Data.Context
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<UserServices.Data.Entity.User> Users { get; set; }
    }
}
