using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErrorCenter.Persistence.EF.Migrations
{
    public partial class IdentityRoleToEnvironment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 27, 22, 8, 30, 644, DateTimeKind.Local).AddTicks(9549),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 6, 12, 193, DateTimeKind.Local).AddTicks(3894));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 27, 22, 8, 30, 644, DateTimeKind.Local).AddTicks(9303),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 6, 12, 193, DateTimeKind.Local).AddTicks(3658));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cf0c2f88-838b-4e6e-b74a-9507adfe0cff", "b2952e00-5551-4f1c-93e5-d68749677fc1", "Development", "DEVELOPMENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca31608a-20e2-4a7e-8d1a-b1fcc72da758", "de813532-c636-4ea0-bb9a-fdf97a244bd1", "Homologation", "HOMOLOGATION" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "065928a7-eae2-4b21-88c7-734432b049af", "ff4863d9-a2d1-486d-8c37-efa191c63cc1", "Production", "PRODUCTION" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "065928a7-eae2-4b21-88c7-734432b049af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca31608a-20e2-4a7e-8d1a-b1fcc72da758");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf0c2f88-838b-4e6e-b74a-9507adfe0cff");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 27, 22, 6, 12, 193, DateTimeKind.Local).AddTicks(3894),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 8, 30, 644, DateTimeKind.Local).AddTicks(9549));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 27, 22, 6, 12, 193, DateTimeKind.Local).AddTicks(3658),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 8, 30, 644, DateTimeKind.Local).AddTicks(9303));

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7388448b-a556-4232-ac46-5ca46b65c89f", "5db80712-09a8-43bf-919a-c34a2742656e", "Development", "DEVELOPMENT" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ac7b87ba-6157-46ac-a3f6-e468f05f120f", "65a663f2-10de-4259-8930-d661a9dfe33c", "Homologation", "HOMOLOGATION" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9554d635-ee07-4eea-862d-b660f6c0c328", "2668d80d-9621-4487-946b-cc83fdd4ea4c", "Production", "PRODUCTION" });
        }
    }
}
