using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TVStation.Data.Constant;
namespace TVStation.Data.Model
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<SiteMap> SiteMap { get; set; }
        /*        public virtual DbSet<Domain.Task> Task { get; set; }
                public virtual DbSet<WorkSchedule> WorkSchedule { get; set; }*/
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>()
            .HasOne(e => e.Creator)
            .WithMany()
            .HasForeignKey("CreatorId")
            .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Event>()
            .HasMany(e => e.Collaborators)
            .WithMany(u => u.CollaboratingEvents)
            .UsingEntity(j => j.ToTable("EventCollaborators"));

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = UserRole.Admin,
                    NormalizedName = UserRole.Admin.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Name = UserRole.Director,
                    NormalizedName = UserRole.Director.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Name = UserRole.Manager,
                    NormalizedName = UserRole.Manager.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Name = UserRole.Reporter,
                    NormalizedName = UserRole.Reporter.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
