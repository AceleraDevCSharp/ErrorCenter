using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErrorCenter.Persistence.EF.Migrations
{
    public partial class FKErrorLogEnvironment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6390c6b2-ee1f-4f81-b8ce-8066bf04feab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ca43722-b3ab-41e9-99a0-9a1cf59d705f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "703758a8-8a2d-461d-8a0c-4d7b49c35e4f");

            migrationBuilder.DropColumn(
                name: "Environment",
                table: "ErrorLogs");

            migrationBuilder.AddColumn<string>(
                name: "EnvironmentID",
                table: "ErrorLogs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 21, 18, 38, 16, 444, DateTimeKind.Local).AddTicks(3422),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 21, 12, 57, 4, 395, DateTimeKind.Local).AddTicks(5385));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 21, 18, 38, 16, 444, DateTimeKind.Local).AddTicks(3171),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 21, 12, 57, 4, 395, DateTimeKind.Local).AddTicks(5099));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
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

            migrationBuilder.CreateIndex(
                name: "IX_ErrorLogs_EnvironmentID",
                table: "ErrorLogs",
                column: "EnvironmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ErrorLogs_AspNetRoles_EnvironmentID",
                table: "ErrorLogs",
                column: "EnvironmentID",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AspNetRoles_EnvironmentID",
                table: "ErrorLogs");

            migrationBuilder.DropIndex(
                name: "IX_ErrorLogs_EnvironmentID",
                table: "ErrorLogs");

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
                name: "EnvironmentID",
                table: "ErrorLogs");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Environment",
                table: "ErrorLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 21, 12, 57, 4, 395, DateTimeKind.Local).AddTicks(5385),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 21, 18, 38, 16, 444, DateTimeKind.Local).AddTicks(3422));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2020, 7, 21, 12, 57, 4, 395, DateTimeKind.Local).AddTicks(5099),
                oldClrType: typeof(DateTime),
                oldNullable: true,
                oldDefaultValue: new DateTime(2020, 7, 21, 18, 38, 16, 444, DateTimeKind.Local).AddTicks(3171));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "703758a8-8a2d-461d-8a0c-4d7b49c35e4f", "1e20c80c-ceb7-40a4-9334-2f9ecb8af556", "Development", "DEVELOPMENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6ca43722-b3ab-41e9-99a0-9a1cf59d705f", "aa4bfa02-fc15-4fd7-813b-e543fad099ac", "Homologation", "HOMOLOGATION" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6390c6b2-ee1f-4f81-b8ce-8066bf04feab", "10e76c38-dd81-469e-a460-f50b749a5b97", "Production", "PRODUCTION" });
        }
    }
}
