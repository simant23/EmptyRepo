using ETS.web.Model.EAnswer;
using ETS.web.Model.EDetails;
using ETS.web.Model.EQuestions;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class EDetailsContext : DbContext
    {
        public EDetailsContext(DbContextOptions<EDetailsContext> options)
        : base(options)
        {

        }
        public DbSet<CEDetails> cEDetails { get; set; }
        public DbSet<UEDetails> uEDetails { get; set; }

        public DbSet<VEDetails> vEDetails { get; set; }
    }
}
