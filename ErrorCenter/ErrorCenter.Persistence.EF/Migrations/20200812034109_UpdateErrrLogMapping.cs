using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErrorCenter.Persistence.EF.Migrations
{
    public partial class UpdateErrrLogMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ErrorLogs_AspNetRoles_EnvironmentID",
                table: "ErrorLogs");

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
                defaultValue: new DateTime(2020, 8, 12, 0, 41, 9, 600, DateTimeKind.Local).AddTicks(4492),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 8, 30, 644, DateTimeKind.Local).AddTicks(9549));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 8, 12, 0, 41, 9, 600, DateTimeKind.Local).AddTicks(4274),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 27, 22, 8, 30, 644, DateTimeKind.Local).AddTicks(9303));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "78ee8980-d61e-4a6e-85fa-5ec98db7402a", "c010fbea-457b-455a-abc3-de857b1abdaa", "Development", "DEVELOPMENT" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1dafd835-56cc-495d-976c-efd32b3db932", "95dbdf16-ae0f-49d0-95e7-1e0f2b52f700", "Homologation", "HOMOLOGATION" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bc37713-c0a7-4d72-84ae-8b4904944e5d", "7562c5fc-19ca-46a9-8c1a-ff98e7f370b9", "Production", "PRODUCTION" });

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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dafd835-56cc-495d-976c-efd32b3db932");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bc37713-c0a7-4d72-84ae-8b4904944e5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78ee8980-d61e-4a6e-85fa-5ec98db7402a");

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
                defaultValue: new DateTime(2020, 7, 27, 22, 8, 30, 644, DateTimeKind.Local).AddTicks(9549),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 12, 0, 41, 9, 600, DateTimeKind.Local).AddTicks(4492));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 27, 22, 8, 30, 644, DateTimeKind.Local).AddTicks(9303),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 8, 12, 0, 41, 9, 600, DateTimeKind.Local).AddTicks(4274));

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
