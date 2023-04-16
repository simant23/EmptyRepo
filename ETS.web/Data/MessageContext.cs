using ETSystem.Model.Message;
using ETSystem.Model.Notice;
using Microsoft.EntityFrameworkCore;

namespace ETS.web.Data
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options)
           : base(options)
        {

        }

        public DbSet<UserDetails> userDetails { get; set; }
        public DbSet<SendMsg> sendMsg { get; set; }
        public DbSet<ReadMsg> readMsg { get; set; }
    }
}
