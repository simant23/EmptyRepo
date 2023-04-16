using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class TeacherDashContext: DbContext
    {
        public TeacherDashContext(DbContextOptions<TeacherDashContext> options)
        : base(options)
        {

        }
    }
}
