﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gIdeas.Models;

namespace gIdeas.Migrations
{
    [DbContext(typeof(gAppDbContext))]
    partial class gAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.HasKey("UserId");

                    b.ToTable("AccessClaims");
                });

            modelBuilder.Entity("gIdeas.Models.gBrowserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrowserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("gBrowserInfos");
                });

            modelBuilder.Entity("gIdeas.Models.gCategoriesToDepartment", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("DepartmentId");

                    b.HasKey("CategoryId", "DepartmentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("gCategoriesToDepartments");
                });

            modelBuilder.Entity("gIdeas.Models.gCategoriesToIdeas", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("IdeaId");

                    b.HasKey("CategoryId", "IdeaId");

                    b.HasIndex("IdeaId");

                    b.ToTable("gCategoriesToIdeas");
                });

            modelBuilder.Entity("gIdeas.Models.gCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("gCategories");
                });

            modelBuilder.Entity("gIdeas.Models.gComments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AnonComment");

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<int>("IdeaId");

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.ToTable("gComments");
                });

            modelBuilder.Entity("gIdeas.Models.gDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("gDepartments");
                });

            modelBuilder.Entity("gIdeas.Models.gDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdeaId");

                    b.Property<string>("Path")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.ToTable("gDocuments");
                });

            modelBuilder.Entity("gIdeas.Models.gFlaggedIdeas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("IdeaId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.ToTable("gFlaggedIdeas");
                });

            modelBuilder.Entity("gIdeas.Models.gIdeas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CloseDate")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 48)))
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 48)))
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("DisplayAnonymous");

                    b.Property<string>("FinalClosureDate")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 48)))
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("gIdeas");
                });

            modelBuilder.Entity("gIdeas.Models.gLastLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FK_gUserId");

                    b.Property<string>("currentLogin");

                    b.Property<string>("lastLogin");

                    b.HasKey("Id");

                    b.HasIndex("FK_gUserId");

                    b.ToTable("gLastLogins");
                });

            modelBuilder.Entity("gIdeas.Models.gRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccessClaim")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("gRoles");
                });

            modelBuilder.Entity("gIdeas.Models.gUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<int>("RoleId");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("RoleId");

                    b.ToTable("gUsers");
                });

            modelBuilder.Entity("gIdeas.Models.gVotes", b =>
                {
                    b.Property<int>("IdeaId");

                    b.Property<int>("UserId");

                    b.Property<string>("Thumb");

                    b.HasKey("IdeaId", "UserId");

                    b.ToTable("gVotes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("gIdeas.Models.gUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gCategoriesToDepartment", b =>
                {
                    b.HasOne("gIdeas.Models.gCategory")
                        .WithMany("Departments")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gIdeas.Models.gDepartment")
                        .WithMany("gCategoriesToDepartments")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gCategoriesToIdeas", b =>
                {
                    b.HasOne("gIdeas.Models.gCategory")
                        .WithMany("Ideas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gIdeas.Models.gIdeas")
                        .WithMany("gCategoriesToIdeas")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gComments", b =>
                {
                    b.HasOne("gIdeas.Models.gIdeas", "Idea")
                        .WithMany("gComments")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gDocument", b =>
                {
                    b.HasOne("gIdeas.Models.gIdeas", "Idea")
                        .WithMany("gDocuments")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gFlaggedIdeas", b =>
                {
                    b.HasOne("gIdeas.Models.gIdeas", "Idea")
                        .WithMany("gFlaggedIdeas")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gIdeas", b =>
                {
                    b.HasOne("gIdeas.Models.gUser", "User")
                        .WithMany("Ideas")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gLastLogin", b =>
                {
                    b.HasOne("gIdeas.Models.gUser", "gUsers")
                        .WithMany("gLastLogin")
                        .HasForeignKey("FK_gUserId");
                });

            modelBuilder.Entity("gIdeas.Models.gUser", b =>
                {
                    b.HasOne("gIdeas.Models.gDepartment", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gIdeas.Models.gRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gVotes", b =>
                {
                    b.HasOne("gIdeas.Models.gIdeas", "Idea")
                        .WithMany("gVotes")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
