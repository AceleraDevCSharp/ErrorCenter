using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErrorCenter.Persistence.EF.Migrations
{
    public partial class UserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9575ce0-d458-4cd1-aaa2-19e965e6c8d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c783079c-b19c-42fe-aa77-4e817d8e60e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db2c84c3-65a8-4643-ae80-cca985d27bd3");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1613),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 21, 18, 38, 16, 444, DateTimeKind.Local).AddTicks(3422));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1341),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 21, 18, 38, 16, 444, DateTimeKind.Local).AddTicks(3171));

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 21, 18, 38, 16, 444, DateTimeKind.Local).AddTicks(3422),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1613));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 21, 18, 38, 16, 444, DateTimeKind.Local).AddTicks(3171),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1341));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "a9575ce0-d458-4cd1-aaa2-19e965e6c8d0", "1d97bedc-cc62-4828-b021-30c7cab3a913", "IdentityRole", "Development", "DEVELOPMENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "db2c84c3-65a8-4643-ae80-cca985d27bd3", "79db5823-b01a-4062-94f4-fc33e6f9b06a", "IdentityRole", "Homologation", "HOMOLOGATION" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "c783079c-b19c-42fe-aa77-4e817d8e60e9", "57375fd1-aef0-48b9-a4d0-3c7969fbd394", "IdentityRole", "Production", "PRODUCTION" });
        }
    }
}
