using ETSystem.Model.User;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {

        }
        public DbSet<Student> student { get; set; }
    }
}
