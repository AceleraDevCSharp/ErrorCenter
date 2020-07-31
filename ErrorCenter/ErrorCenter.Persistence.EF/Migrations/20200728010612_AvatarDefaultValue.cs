using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErrorCenter.Persistence.EF.Migrations
{
    public partial class AvatarDefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 27, 22, 6, 12, 193, DateTimeKind.Local).AddTicks(3894),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1613));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 27, 22, 6, 12, 193, DateTimeKind.Local).AddTicks(3658),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1341));

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: "default.png",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7388448b-a556-4232-ac46-5ca46b65c89f");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9554d635-ee07-4eea-862d-b660f6c0c328");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ac7b87ba-6157-46ac-a3f6-e468f05f120f");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1613),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 6, 12, 193, DateTimeKind.Local).AddTicks(3894));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 23, 14, 31, 2, 767, DateTimeKind.Local).AddTicks(1341),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 6, 12, 193, DateTimeKind.Local).AddTicks(3658));

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "default.png");

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
    }
}
