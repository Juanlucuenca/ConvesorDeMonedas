using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConvesorDeMonedas.Migrations
{
    /// <inheritdoc />
    public partial class fixedAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmoutOfConvertions",
                table: "Subscriptions",
                newName: "AmountOfConvertions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountOfConvertions",
                table: "Subscriptions",
                newName: "AmoutOfConvertions");
        }
    }
}
