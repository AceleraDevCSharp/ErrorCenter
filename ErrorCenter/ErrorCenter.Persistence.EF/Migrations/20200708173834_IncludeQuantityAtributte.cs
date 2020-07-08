﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErrorCenter.Persistence.EF.Migrations
{
    public partial class IncludeQuantityAtributte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "ErrorLogs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArquivedAt",
                table: "ErrorLogs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ErrorLogs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ErrorLogs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "ErrorLogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ArquivedAt",
                table: "ErrorLogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
