using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConvesorDeMonedas.Migrations
{
    /// <inheritdoc />
    public partial class addcol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ConvertedPrice",
                table: "Conversions",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceToConvert",
                table: "Conversions",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConvertedPrice",
                table: "Conversions");

            migrationBuilder.DropColumn(
                name: "PriceToConvert",
                table: "Conversions");
        }
    }
}
