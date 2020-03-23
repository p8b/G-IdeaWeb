﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gIdeas.Models;

namespace gIdeas.Migrations
{
    [DbContext(typeof(gAppDbContext))]
    [Migration("20200323084005_v0.3")]
    partial class v03
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("gIdeas.Models.FlaggedIdea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("IdeaId");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.Property<int>("UsersId");

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.HasIndex("UsersId");

                    b.ToTable("FlaggedIdeas");
                });

            modelBuilder.Entity("gIdeas.Models.gCategoriesToIdeas", b =>
                {
                    b.Property<int>("CategoryId");

                    b.Property<int>("IdeaId");

                    b.HasKey("CategoryId", "IdeaId");

                    b.HasIndex("IdeaId");

                    b.ToTable("CategoriesToIdeas");
                });

            modelBuilder.Entity("gIdeas.Models.gCategoryTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("gIdeas.Models.gClosureDates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FinalClosure");

                    b.Property<int>("FirstClosure");

                    b.Property<string>("TimeStampLastModified")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 48)))
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("ClosureDates");
                });

            modelBuilder.Entity("gIdeas.Models.gComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<int>("IdeaId");

                    b.Property<bool>("IsAnonymous");

                    b.Property<string>("SubmissionDate")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 48)))
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("IdeaId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("gIdeas.Models.gDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Departments");
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

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("gIdeas.Models.gIdea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorId");

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

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Ideas");
                });

            modelBuilder.Entity("gIdeas.Models.gLoginRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrowserName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TimeStamp")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 48)))
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginRecords");
                });

            modelBuilder.Entity("gIdeas.Models.gPageView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PageCount");

                    b.Property<string>("PageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("PageViews");
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

                    b.ToTable("Roles");
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

                    b.Property<bool>("IsBlocked");

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

                    b.ToTable("Users");
                });

            modelBuilder.Entity("gIdeas.Models.gVotes", b =>
                {
                    b.Property<int>("IdeaId");

                    b.Property<int>("UserId");

                    b.Property<string>("Thumb");

                    b.HasKey("IdeaId", "UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("gIdeas.Models.gUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.FlaggedIdea", b =>
                {
                    b.HasOne("gIdeas.Models.gIdea", "Idea")
                        .WithMany("gFlaggedIdeas")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gIdeas.Models.gUser", "Users")
                        .WithMany("FlaggedIdeas")
                        .HasForeignKey("UsersId");
                });

            modelBuilder.Entity("gIdeas.Models.gCategoriesToIdeas", b =>
                {
                    b.HasOne("gIdeas.Models.gCategoryTag")
                        .WithMany("Ideas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gIdeas.Models.gIdea")
                        .WithMany("gCategoriesToIdeas")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gComment", b =>
                {
                    b.HasOne("gIdeas.Models.gIdea", "Idea")
                        .WithMany("gComments")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("gIdeas.Models.gUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("gIdeas.Models.gDocument", b =>
                {
                    b.HasOne("gIdeas.Models.gIdea", "Idea")
                        .WithMany("gDocuments")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gIdea", b =>
                {
                    b.HasOne("gIdeas.Models.gUser", "Author")
                        .WithMany("Ideas")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("gIdeas.Models.gLoginRecord", b =>
                {
                    b.HasOne("gIdeas.Models.gUser", "User")
                        .WithMany("loginRecords")
                        .HasForeignKey("UserId");
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
                    b.HasOne("gIdeas.Models.gIdea")
                        .WithMany("gVotes")
                        .HasForeignKey("IdeaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}