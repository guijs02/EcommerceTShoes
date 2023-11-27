using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class delColumnValorTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Carrinho");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Carrinho",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
