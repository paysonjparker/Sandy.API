using Microsoft.EntityFrameworkCore;
using Sandy.Models.DataTransferObjects.GolferDto;
using Sandy.Models.DataTransferObjects.ScoreDto;

namespace Sandy.Data
{
    public class SandyDbContext : DbContext
    {
        public SandyDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}
