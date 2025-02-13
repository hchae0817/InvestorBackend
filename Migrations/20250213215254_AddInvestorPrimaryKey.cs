using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestorPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Investors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvestorName = table.Column<string>(type: "TEXT", nullable: true),
                    InvestorType = table.Column<string>(type: "TEXT", nullable: true),
                    InvestorCountry = table.Column<string>(type: "TEXT", nullable: true),
                    InvestorDateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InvestorLastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CommitmentAssetClass = table.Column<string>(type: "TEXT", nullable: true),
                    CommitmentAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    CommitmentCurrency = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investors");
        }
    }
}
