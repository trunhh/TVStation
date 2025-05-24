using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public virtual DbSet<Programme> Programme { get; set; }
        public virtual DbSet<Episode> Episode { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d)
            );

            builder.Entity<Programme>()
                .Property(h => h.StartDate)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("date");

            var timeOnlyConverter = new ValueConverter<TimeOnly, TimeSpan>(
            t => t.ToTimeSpan(),
            t => TimeOnly.FromTimeSpan(t)
        );

            builder.Entity<Programme>()
                .Property(s => s.StartTime)
                .HasConversion(timeOnlyConverter)
                .HasColumnType("time");

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
                },
                new IdentityRole
                {
                    Name = UserRole.VideoEditor,
                    NormalizedName = UserRole.VideoEditor.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Name = UserRole.ScreenWriter,
                    NormalizedName = UserRole.ScreenWriter.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
