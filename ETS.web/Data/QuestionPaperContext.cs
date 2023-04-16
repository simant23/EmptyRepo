using ETSystem.Model.QuestionPaper;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class QuestionPaperContext : DbContext
    {
        public QuestionPaperContext(DbContextOptions<QuestionPaperContext> options)
           : base(options)
        {

        }
        public DbSet<QuestionPaper> questionPaper { get; set; }

        public DbSet<ViewPaper> viewPaper { get; set; }

        public DbSet<Paper> paper { get; set; }




    }
}
