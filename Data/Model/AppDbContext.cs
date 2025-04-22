using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TVStation.Data.Constant;
using TVStation.Data.Model.Plans.Productions;
using TVStation.Data.Model.Plans.ProgramFrames;
namespace TVStation.Data.Model
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<SiteMap> SiteMap { get; set; }
        /*        public virtual DbSet<Domain.Task> Task { get; set; }
                public virtual DbSet<WorkSchedule> WorkSchedule { get; set; }*/
        public virtual DbSet<Event> ProgramFrameYear { get; set; }
        public virtual DbSet<ProgramFrameWeek> ProgramFrameWeek { get; set; }
        public virtual DbSet<ProgramFrameBroadcast> ProgramFrameBroadcast { get; set; }
        public virtual DbSet<ProductionRegistration> ProductionRegistration { get; set; }
        public virtual DbSet<ScriptProgram> ScriptProgram { get; set; }
        public virtual DbSet<MediaProject> MediaProject { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
