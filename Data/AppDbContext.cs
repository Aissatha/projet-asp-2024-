using Microsoft.EntityFrameworkCore;
using StudentPortail.Web.Models.Entities;

namespace StudentPortail.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
