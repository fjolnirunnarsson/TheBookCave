using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheBookCave.Migrations
{
    public partial class addingAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false),
                    BillingAddressCity = table.Column<string>(nullable: true),
                    BillingAddressCountry = table.Column<string>(nullable: true),
                    BillingAddressHouseNumber = table.Column<int>(nullable: false),
                    BillingAddressLine2 = table.Column<string>(nullable: true),
                    BillingAddressStreet = table.Column<string>(nullable: true),
                    BillingAddressZipCode = table.Column<string>(nullable: true),
                    DeliveryAddressCity = table.Column<string>(nullable: true),
                    DeliveryAddressCountry = table.Column<string>(nullable: true),
                    DeliveryAddressHouseNumber = table.Column<int>(nullable: false),
                    DeliveryAddressLine2 = table.Column<string>(nullable: true),
                    DeliveryAddressStreet = table.Column<string>(nullable: true),
                    DeliveryAddressZipCode = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
