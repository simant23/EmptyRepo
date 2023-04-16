using ETSystem.Model.Notice;
using ETSystem.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
        {

        }
        public DbSet<User> user { get; set; }
    }
}
