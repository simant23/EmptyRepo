using ETS.web.Model.Question;
using ETSystem.Model.Notice;
using ETSystem.Model.Question;
using ETSystem.Model.SampleQuestion;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class ObjQContext : DbContext
    {
        public ObjQContext(DbContextOptions<ObjQContext> options)
           : base(options)
        {

        }
        
        public DbSet<ObjQ> objective { get; set; }
        public DbSet<UpdateQ> obj { get; set; }

        public DbSet<ViewQuestions> viewQuestions { get; set; }


    }
}
