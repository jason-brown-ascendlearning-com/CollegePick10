﻿// <auto-generated />
using System;
using CollegePick10V2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CollegePick10V2.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220714222825_AddGameArchive")]
    partial class AddGameArchive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CollegePick10V2.Entities.CurrentWeek", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FavoriteID")
                        .HasColumnType("int");

                    b.Property<int?>("FavoriteScore")
                        .HasColumnType("int");

                    b.Property<string>("GameProgress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("GameTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("GameUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HomeTeam")
                        .HasColumnType("int");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

                    b.Property<decimal>("OverUnder")
                        .HasColumnType("decimal(3,1)");

                    b.Property<decimal>("PointSpread")
                        .HasColumnType("decimal(3,1)");

                    b.Property<int>("UnderDogID")
                        .HasColumnType("int");

                    b.Property<int?>("UnderdogScore")
                        .HasColumnType("int");

                    b.HasKey("GameID");

                    b.ToTable("CurrentWeeks");

                    b.HasData(
                        new
                        {
                            GameID = 1,
                            FavoriteID = 1,
                            GameTime = new DateTime(2022, 7, 14, 17, 28, 24, 836, DateTimeKind.Local).AddTicks(848),
                            HomeTeam = 1,
                            Locked = false,
                            OverUnder = 45.5m,
                            PointSpread = 21.5m,
                            UnderDogID = 2
                        },
                        new
                        {
                            GameID = 2,
                            FavoriteID = 2,
                            GameTime = new DateTime(2022, 7, 14, 17, 28, 24, 841, DateTimeKind.Local).AddTicks(7315),
                            HomeTeam = 2,
                            Locked = false,
                            OverUnder = 52m,
                            PointSpread = 23m,
                            UnderDogID = 1
                        });
                });

            modelBuilder.Entity("CollegePick10V2.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CollegePick10V2.Entities.GameArchive", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FavoriteID")
                        .HasColumnType("int");

                    b.Property<int>("FavoriteScore")
                        .HasColumnType("int");

                    b.Property<DateTime>("GameTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("HomeTeam")
                        .HasColumnType("int");

                    b.Property<decimal>("OverUnder")
                        .HasColumnType("decimal(3,1)");

                    b.Property<decimal>("PointSpread")
                        .HasColumnType("decimal(3,1)");

                    b.Property<int>("UnderDogID")
                        .HasColumnType("int");

                    b.Property<int>("UnderdogScore")
                        .HasColumnType("int");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.HasKey("GameID");

                    b.ToTable("GameArchives");
                });

            modelBuilder.Entity("CollegePick10V2.Entities.Pick", b =>
                {
                    b.Property<int>("PickID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CorrectPoints")
                        .HasColumnType("int");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<int>("PickTypeID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PickID");

                    b.ToTable("Picks");
                });

            modelBuilder.Entity("CollegePick10V2.Entities.PickArchive", b =>
                {
                    b.Property<int>("PickID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CorrectPoints")
                        .HasColumnType("int");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<int>("PickTypeID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WeekNumber")
                        .HasColumnType("int");

                    b.HasKey("PickID");

                    b.ToTable("PickArchives");
                });

            modelBuilder.Entity("CollegePick10V2.Entities.PickType", b =>
                {
                    b.Property<int>("PickTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PickTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PickTypeID");

                    b.ToTable("PickTypes");
                });

            modelBuilder.Entity("CollegePick10V2.Entities.Teams", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EspnName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FulleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rank")
                        .HasColumnType("int");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            TeamID = 1,
                            Abbreviation = "ABCHRST",
                            EspnName = "Abilene Christian",
                            FulleName = "Abilene Christian Wildcats"
                        },
                        new
                        {
                            TeamID = 2,
                            Abbreviation = "AF",
                            EspnName = "Air Force",
                            FulleName = "Air Force Falcons"
                        });
                });

            modelBuilder.Entity("CollegePick10V2.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("PaidAmount")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CollegePick10V2.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CollegePick10V2.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CollegePick10V2.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CollegePick10V2.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
