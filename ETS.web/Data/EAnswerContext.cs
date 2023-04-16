using ETS.web.Model.EAnswer;
using ETS.web.Model.EQuestions;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class EAnswerContext:DbContext
    {
        public EAnswerContext(DbContextOptions<EAnswerContext> options)
        : base(options)
        {

        }
        public DbSet<CreateAnswer> createAnswer { get; set; }
        public DbSet<ViewAnswer> viewAnswer { get; set; }
        //public DbSet<Check> check { get; set; }
    }
}
