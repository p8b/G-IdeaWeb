using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace gIdeas.Models
{
    public class gAppDbContext : IdentityUserContext<gUser, int, 
        IdentityUserClaim<int>, 
        IdentityUserLogin<int>, 
        IdentityUserToken<int>>
    {
        ///     Properties to access the database tables.
        ///     Here the name of the attribute will be the 
        ///     name of the table in db.
        public DbSet<gRole> Roles { get; set; }
        public DbSet<gCategoryTag> Categories { get; set; }
        public DbSet<gDepartment> Departments { get; set; }
        public DbSet<gComment> Comments { get; set; }
        //public DbSet<gDocument> Documents { get; set; }
        public DbSet<gFlaggedIdea> FlaggedIdeas { get; set; }
        public DbSet<gIdea> Ideas { get; set; }
        public DbSet<gVotes> Votes { get; set; }
        public DbSet<gLoginRecord> LoginRecords { get; set; }
        public DbSet<gClosureDates> ClosureDates { get; set; }
        public DbSet<gPageView> PageViews { get; set; }

        public DbSet<gCategoriesToIdeas> CategoriesToIdeas { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            /// Configure identity framework schema
            base.OnModelCreating(builder);

            #region *** Remove unnecessary Entities added by Identity User Context class ***
            builder.Ignore<IdentityUserLogin<int>>();
            builder.Ignore<IdentityUserToken<int>>();
            #endregion

            #region *** Change table names ***
            builder.Entity<gUser>().ToTable("Users");
            builder.Entity<IdentityUserClaim<int>>().ToTable("AccessClaims");
            #endregion

            builder.Entity<IdentityUserClaim<int>>().Ignore("Id");
            builder.Entity<IdentityUserClaim<int>>().HasKey("UserId");

            /// Make the composite keys for the link table between the category and department
            builder.Entity<gCategoriesToIdeas>().HasKey(ci => new { ci.CategoryId, ci.IdeaId});
            builder.Entity<gVotes>().HasKey(v => new {v.IdeaId , v.UserId});

            builder.Entity<gUser>().HasMany(u => u.Comments).WithOne(c=>c.User).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<gUser>().HasMany(u => u.FlaggedIdeas).WithOne(f=>f.Users).OnDelete(DeleteBehavior.ClientSetNull);
            #region *** Remove unnecessary attributes form IdentityUser class ***
            builder.Entity<gUser>().Ignore(u => u.EmailConfirmed);
            builder.Entity<gUser>().Ignore(u => u.LockoutEnabled);
            builder.Entity<gUser>().Ignore(u => u.LockoutEnd);
            builder.Entity<gUser>().Ignore(u => u.AccessFailedCount);
            builder.Entity<gUser>().Ignore(u => u.PhoneNumber);
            builder.Entity<gUser>().Ignore(u => u.UserName);
            builder.Entity<gUser>().Ignore(u => u.NormalizedUserName);
            builder.Entity<gUser>().Ignore(u => u.PhoneNumberConfirmed);
            builder.Entity<gUser>().Ignore(u => u.TwoFactorEnabled);
            #endregion
        }
        public gAppDbContext(DbContextOptions<gAppDbContext> options)
          : base(options)
        {
        }
    }
}
