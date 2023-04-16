using ETSystem.Model.Notice;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class NoticeContext : DbContext
    {
        public NoticeContext(DbContextOptions<NoticeContext> options)
            : base(options)
        {

        }
        public DbSet<Notice> notice { get; set; }
        public DbSet<CreateNotice> cnotice { get; set; }


    }
}
