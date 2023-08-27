using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepartManagment.Persistence.Migrations
{
    public partial class AddModelsnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeTasks",
                keyColumn: "Id",
                keyValue: new Guid("cffbe462-5ae3-40c6-b26c-9ffc255212d8"));

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: "e04224fc-9fe4-4e1c-8870-383f4381a19b");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("4bfecb97-2091-40cf-a446-23fdd8a2f7a8"));

            migrationBuilder.DropColumn(
                name: "IsManager",
                table: "employees");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreationDate", "ManagerId", "Name" },
                values: new object[] { new Guid("93f370bb-f8f8-40cc-ba8c-3e606752aef2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IT Department" });

            migrationBuilder.InsertData(
                table: "EmployeeTasks",
                columns: new[] { "Id", "CreationDate", "Description", "EmployeeId", "IsCompleted", "Title" },
                values: new object[] { new Guid("7476880c-1ea9-4282-bf2a-07df04e17a4c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finish the project by the end of the month", null, false, "Complete Project X" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "Gender", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5efffc49-fd6d-4ee4-aa15-86b151097b64", 0, "f6d1d8a5-a157-4d31-978a-2b225047148a", new Guid("93f370bb-f8f8-40cc-ba8c-3e606752aef2"), "Leqaa.Technical@gmail.com", true, 1, false, null, "Leqaa", "LEQAA.TECHNICAL@GMAIL.COM", "LEQAA", "AQAAAAEAACcQAAAAEHMH5fKjfbQWIjWVVdD/nVVB5tYEGDkPm9bxGQdS2LxOkhGONJUdTBrnudcyb265xg==", null, false, 0, "16e0681f-9c8c-4f71-b29d-be437c90f2bd", false, "Leqaa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeTasks",
                keyColumn: "Id",
                keyValue: new Guid("7476880c-1ea9-4282-bf2a-07df04e17a4c"));

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: "5efffc49-fd6d-4ee4-aa15-86b151097b64");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("93f370bb-f8f8-40cc-ba8c-3e606752aef2"));

            migrationBuilder.DropColumn(
                name: "Role",
                table: "employees");

            migrationBuilder.AddColumn<bool>(
                name: "IsManager",
                table: "employees",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreationDate", "ManagerId", "Name" },
                values: new object[] { new Guid("4bfecb97-2091-40cf-a446-23fdd8a2f7a8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "IT Department" });

            migrationBuilder.InsertData(
                table: "EmployeeTasks",
                columns: new[] { "Id", "CreationDate", "Description", "EmployeeId", "IsCompleted", "Title" },
                values: new object[] { new Guid("cffbe462-5ae3-40c6-b26c-9ffc255212d8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Finish the project by the end of the month", null, false, "Complete Project X" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "Gender", "IsManager", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e04224fc-9fe4-4e1c-8870-383f4381a19b", 0, "6c2d690d-3bcb-4691-99ac-c1796843e334", new Guid("4bfecb97-2091-40cf-a446-23fdd8a2f7a8"), "Leqaa.Technical@gmail.com", true, 1, true, false, null, "Leqaa", "LEQAA.TECHNICAL@GMAIL.COM", "LEQAA", "AQAAAAEAACcQAAAAEMRFrkNkhtXJhtE6uiNgaBcskrIH2XLdnSjtYDHbY9XGZ5UEmnIwrnsvwNcPl5s1+w==", null, false, "2c923e74-d90f-4d52-a994-de76ffed70d0", false, "Leqaa" });
        }
    }
}
