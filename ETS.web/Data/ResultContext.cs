using ETS.web.Model.Result;
using ETSystem.Model.Notice;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class ResultContext : DbContext
    {
        public ResultContext(DbContextOptions<ResultContext> options)
        : base(options)
        {

        }

        public DbSet<AnswerResultView> AresultView { get; set; }
        public DbSet<Mark> mark { get; set; }
        public DbSet<Markobtained> markobtained { get; set; }
        public DbSet<ResultList> resultLists { get; set; }
    }
}
