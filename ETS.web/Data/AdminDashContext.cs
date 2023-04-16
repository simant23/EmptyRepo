using ETS.web.Model.Dashboard;
using ETSystem.Model.Notice;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class AdminDashContext : DbContext
    {
        public AdminDashContext(DbContextOptions<AdminDashContext> options)
        : base(options)
        {

        }
        public DbSet<AdminDash> adminDashs { get; set; }
        public DbSet<ATeacher> aTeacher { get; set; }
        public DbSet<TotalExam> totalExam { get; set; }
        public DbSet<TSQn> tSQn { get; set; }
        public DbSet<Activeness> activeness { get; set; }
    }
}
