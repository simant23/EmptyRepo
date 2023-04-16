using ETS.web.Model.EQuestions;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ETS.web.Data
{
    public class EQuestionsContext: DbContext
    {
        public EQuestionsContext(DbContextOptions<EQuestionsContext> options)
            : base(options)
        {

        }
        public DbSet<CreateQuestions> createQuestions { get; set; }
        public DbSet<UpdateQuestions> updateQuestions { get; set; }

        public DbSet<ViewEQuestions> viewEQuestions { get; set; }
        public DbSet<QnStatus> qnStatus { get; set; }
    }
}
