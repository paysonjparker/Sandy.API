using Microsoft.EntityFrameworkCore;
using Sandy.API.Models.DomainModels;
using Sandy.Models.DomainModels;

namespace Sandy.Data
{
    public class SandyDbContext : DbContext
    {
        public SandyDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<GolfCourse> GolfCourses { get; set; }

    }
}
