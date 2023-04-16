using ETSystem.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class TeacherContext : DbContext
    {
        public TeacherContext(DbContextOptions<TeacherContext> options)
            : base(options)
        {

        }
        public DbSet<Teacher> teacher { get; set; }
    }
}
