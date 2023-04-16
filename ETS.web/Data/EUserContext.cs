using ETSystem.Model.Notice;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class EUserContext : DbContext
    {
        public EUserContext(DbContextOptions<EUserContext> options)
            : base(options)
        {

        }
        public DbSet<EUser> eUser { get; set; }
    }
}
