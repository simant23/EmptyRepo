using ETS.web.Model.TExam;
using ETSystem.Model.TExam;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class TExamContext : DbContext
    {
        public TExamContext(DbContextOptions<TExamContext> options)
            : base(options)
        {

        }

        public DbSet<TeacherExam> teacherExam { get; set; }
        public DbSet<UpdateExam> updateExam { get; set; }
        public DbSet<ViewExam> Exam { get; set; }
        public DbSet<Exam> exam { get; set; }
        public DbSet<ExamSt> ExamSt { get; set; }

    }
}
