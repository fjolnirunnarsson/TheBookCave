using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheBookCave.Migrations
{
    public partial class SameAddresses2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SameAdresses",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SameAdresses",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
