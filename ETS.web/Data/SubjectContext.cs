using ETSystem.Model.Notice;
using ETSystem.Model.QuestionPaper;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class SubjectContext : DbContext
    {
        public SubjectContext(DbContextOptions<SubjectContext> options)
            : base(options)
        {

        }
        public DbSet<Subject> subject { get; set; }
    }
}
