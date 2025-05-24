using Microsoft.EntityFrameworkCore;
using NewsPlatformApp.Models;

namespace NewsPlatformApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
