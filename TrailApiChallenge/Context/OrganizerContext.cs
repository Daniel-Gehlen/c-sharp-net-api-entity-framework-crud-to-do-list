using Microsoft.EntityFrameworkCore;
using TrailApiChallenge.Models;

namespace TrailApiChallenge.Context
{
    public class OrganizerContext : DbContext
    {
        public OrganizerContext(DbContextOptions<OrganizerContext> options) : base(options)
        {
            
        }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}
