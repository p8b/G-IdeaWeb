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
        public DbSet<gRole> gRoles { get; set; }
        public DbSet<gCategory> gCategories { get; set; }
        public DbSet<gDepartment> gDepartments { get; set; }
        public DbSet<gComments> gComments { get; set; }
        public DbSet<gDocument> gDocuments { get; set; }
        public DbSet<gFlaggedIdeas> gFlaggedIdeas { get; set; }
        public DbSet<gIdeas> gIdeas { get; set; }
        public DbSet<gVotes> gVotes { get; set; }

        public DbSet<gCategoriesToDepartment> gCategoriesToDepartments { get; set; }
        public DbSet<gCategoriesToIdeas> gCategoriesToIdeas { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            /// Configure identity framework schema
            base.OnModelCreating(builder);

            #region *** Remove unnecessary Entities added by Identity User Context class ***
            builder.Ignore<IdentityUserLogin<int>>();
            builder.Ignore<IdentityUserToken<int>>();
            #endregion

            #region *** Change table names ***
            builder.Entity<gUser>().ToTable("gUsers");
            builder.Entity<IdentityUserClaim<int>>().ToTable("AccessClaims");
            #endregion

            builder.Entity<IdentityUserClaim<int>>().Ignore("Id");
            builder.Entity<IdentityUserClaim<int>>().HasKey("UserId");

            /// Make the composite keys for the link table between the category and department
            builder.Entity<gCategoriesToDepartment>().HasKey(cd => new { cd.CategoryId, cd.DepartmentId });
            builder.Entity<gCategoriesToIdeas>().HasKey(ci => new { ci.CategoryId, ci.IdeaId});
            builder.Entity<gVotes>().HasKey(v => new {v.IdeaId , v.UserId});

            #region *** Remove unnecessary attributes form IdentityUser class ***
                builder.Entity<gUser>().Ignore(u => u.EmailConfirmed);
            builder.Entity<gUser>().Ignore(u => u.SecurityStamp);
            builder.Entity<gUser>().Ignore(u => u.ConcurrencyStamp);
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
