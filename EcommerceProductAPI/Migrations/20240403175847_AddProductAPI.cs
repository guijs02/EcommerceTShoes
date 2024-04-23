using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcommerceProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAPI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamanho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Descricao", "Genero", "ImagemUrl", "Nome", "Preco", "Tamanho" },
                values: new object[,]
                {
                    { 1, "tenis", 2, "/assets/VANS.webp", "VANS Preto", 199.99m, "" },
                    { 2, "tenis", 2, "/assets/aaaaa.jpg", "Adidas Gold", 199.99m, "" },
                    { 3, "tenis", 2, "/assets/MaxRosa.webp", "Air max rosa", 199.99m, "" },
                    { 4, "tenis", 2, "/assets/tenis-nike-downshifter-11-feminino-img.jpg", "Nike Purple Run", 199.99m, "" },
                    { 5, "tenis", 1, "/assets/tenisPuma.webp", "Puma Branco", 289.99m, "" },
                    { 6, "tenis", 1, "/assets/ct0978-060_4.jpg", "Nike Air Jordan", 289.99m, "" },
                    { 7, "tenis", 1, "/assets/1011B004_300_SR_RT_GLB_PNG_1280x1280-JPG.webp", " Asics Verde", 249.99m, "" },
                    { 9, "tenis", 1, "/assets/MaxColorido.webp", "Air Max Colorido", 349.99m, "" },
                    { 10, "tenis", 1, "/assets/puma-suede-preto.png", "Puma Suede", 349.99m, "" },
                    { 11, "tenis", 2, "/assets/Air-Max-preto-feminino.png", "Air Max preto", 489.99m, "" },
                    { 12, "tenis", 1, "/assets/nike-preto24.png", "Nike preto 2k 24", 309.99m, "" },
                    { 13, "tenis", 1, "/assets/new-balance-feminino.png", "Nike preto 2k 24", 248.99m, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
