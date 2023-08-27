﻿// <auto-generated />
using System;
using DepartManagment.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DepartManagment.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230827000012_AddModelsnew")]
    partial class AddModelsnew
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DepartManagment.Domain.Entities.ApplicationUser.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("employees", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5efffc49-fd6d-4ee4-aa15-86b151097b64",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f6d1d8a5-a157-4d31-978a-2b225047148a",
                            DepartmentId = new Guid("93f370bb-f8f8-40cc-ba8c-3e606752aef2"),
                            Email = "Leqaa.Technical@gmail.com",
                            EmailConfirmed = true,
                            Gender = 1,
                            LockoutEnabled = false,
                            Name = "Leqaa",
                            NormalizedEmail = "LEQAA.TECHNICAL@GMAIL.COM",
                            NormalizedUserName = "LEQAA",
                            PasswordHash = "AQAAAAEAACcQAAAAEHMH5fKjfbQWIjWVVdD/nVVB5tYEGDkPm9bxGQdS2LxOkhGONJUdTBrnudcyb265xg==",
                            PhoneNumberConfirmed = false,
                            Role = 0,
                            SecurityStamp = "16e0681f-9c8c-4f71-b29d-be437c90f2bd",
                            TwoFactorEnabled = false,
                            UserName = "Leqaa"
                        });
                });

            modelBuilder.Entity("DepartManagment.Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ManagerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId")
                        .IsUnique();

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("93f370bb-f8f8-40cc-ba8c-3e606752aef2"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "IT Department"
                        });
                });

            modelBuilder.Entity("DepartManagment.Domain.Entities.EmployeeTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeTasks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7476880c-1ea9-4282-bf2a-07df04e17a4c"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Finish the project by the end of the month",
                            IsCompleted = false,
                            Title = "Complete Project X"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("roleclaims", (string)null);
                });

            modelBuilder.Entity("DepartManagment.Domain.Entities.ApplicationUser.Employee", b =>
                {
                    b.HasOne("DepartManagment.Domain.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DepartManagment.Domain.Entities.Department", b =>
                {
                    b.HasOne("DepartManagment.Domain.Entities.ApplicationUser.Employee", "Manager")
                        .WithOne()
                        .HasForeignKey("DepartManagment.Domain.Entities.Department", "ManagerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("DepartManagment.Domain.Entities.EmployeeTask", b =>
                {
                    b.HasOne("DepartManagment.Domain.Entities.ApplicationUser.Employee", "Employee")
                        .WithMany("Tasks")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DepartManagment.Domain.Entities.ApplicationUser.Employee", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("DepartManagment.Domain.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
