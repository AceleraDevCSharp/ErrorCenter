using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErrorCenter.Persistence.EF.Migrations
{
    public partial class UpdateErrorLogMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AspNetRoles_EnvironmentID",
                table: "ErrorLogs");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "0bbf8e1b-8313-4d7c-8a5a-057ad5065456");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "64cea744-c0c9-4fe4-b5b0-aca75c4ac09a");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "e1826ed5-8636-452b-9dbc-a6a23f7e7128");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ErrorLogs",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Origin",
                table: "ErrorLogs",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "ErrorLogs",
                type: "varchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EnvironmentID",
                table: "ErrorLogs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "ErrorLogs",
                type: "varchar(1500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ErrorLogs",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 28, 23, 27, 35, 903, DateTimeKind.Local).AddTicks(3871),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1613));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 28, 23, 27, 35, 903, DateTimeKind.Local).AddTicks(3587),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1341));

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fff2dd4f-3d23-4918-9de4-cf79e1e6d158", "a5d639f2-f9dc-4f82-8411-b0bd73ee6407", "Development", "DEVELOPMENT" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "af24af07-0ec9-4972-95c0-e3684fa65988", "4b9ffdd3-1972-4188-91f1-e3f98bdb576c", "Homologation", "HOMOLOGATION" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d7f08a4d-5d42-4856-8a88-1546a95a299f", "7e4b71ea-7993-4e98-b2c7-f7b2a131b115", "Production", "PRODUCTION" });

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_AspNetRoles_EnvironmentID",
                table: "ErrorLogs",
                column: "EnvironmentID",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AspNetRoles_EnvironmentID",
                table: "ErrorLogs");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "af24af07-0ec9-4972-95c0-e3684fa65988");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d7f08a4d-5d42-4856-8a88-1546a95a299f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fff2dd4f-3d23-4918-9de4-cf79e1e6d158");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ErrorLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Origin",
                table: "ErrorLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "ErrorLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "EnvironmentID",
                table: "ErrorLogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Details",
                table: "ErrorLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1500)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ErrorLogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1613),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 28, 23, 27, 35, 903, DateTimeKind.Local).AddTicks(3871));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1341),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 28, 23, 27, 35, 903, DateTimeKind.Local).AddTicks(3587));

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e1826ed5-8636-452b-9dbc-a6a23f7e7128", "f62e54a1-2b16-43d6-ade8-073ecc8c0f8f", "Development", "DEVELOPMENT" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0bbf8e1b-8313-4d7c-8a5a-057ad5065456", "49325e89-2651-49d2-9fdb-89fd2d632043", "Homologation", "HOMOLOGATION" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64cea744-c0c9-4fe4-b5b0-aca75c4ac09a", "d5270177-a09c-4553-88ed-d80efe23b4ea", "Production", "PRODUCTION" });

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_AspNetRoles_EnvironmentID",
                table: "ErrorLogs",
                column: "EnvironmentID",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
