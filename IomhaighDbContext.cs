using iomhiagh_api.Model;
using Microsoft.EntityFrameworkCore;

namespace iomhiagh_api
{
    public class IomhaighDbContext : DbContext
    {
        public IomhaighDbContext(DbContextOptions<IomhaighDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Skill>()
                            .HasOne(x => x.Parent)
                            .WithMany(x => x.Children);
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillEvent> SkillEvents { get; set; }
    }
}
