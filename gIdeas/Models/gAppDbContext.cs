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
        public DbSet<gCategoriesToDepartment> gCategoriesToDepartments { get; set; }
        public DbSet<gCategory> gCategories { get; set; }
        public DbSet<gDepartment> gDepartments { get; set; }


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

            //builder.Entity<gCategoriesToDepartment>().HasKey(cd => { cd.Category})

            //builder.Entity<gCategoriesToDepartment>(cd =>
            //{
            //    cd.HasOne(c => c.Category.id)
            //    .WithOne (c => c.Id)
            //});

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
