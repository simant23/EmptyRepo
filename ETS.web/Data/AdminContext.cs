using ETSystem.Model.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ETS.web.Data
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options)
        : base(options)
        {

        }
        public DbSet<Admin> admin { get; set; }
    }
}
