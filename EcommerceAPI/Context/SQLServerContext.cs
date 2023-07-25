using EcommerceTShoes.Model;
using LoginAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Context
{
    public class SQLServerContext : IdentityDbContext<Usuario>
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) { }
        public DbSet<Produto> Products { get; set; }
        public DbSet<CarrinhoDeCompra> Carrinho { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            
            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 1,
                Nome = "Tenis VANS Preto",
                Descricao = "tenis",
                Preco = 199.99m,
                ImagemUrl = "/assets/VANS.webp",
                Genero = EGenero.Feminino,
                Tamanho= string.Empty
            });
            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 2,
                Nome = "Tenis adidas",
                Descricao = "tenis",
                Preco = 199.99m,
                ImagemUrl = "/assets/aaaaa.jpg",
                Genero = EGenero.Feminino,
               Tamanho = string.Empty
            });
            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 3,
                Nome = "Air max rosa",
                Descricao = "tenis",
                Preco = 199.99m,
                ImagemUrl = "/assets/MaxRosa.webp",
                Genero = EGenero.Feminino,
                Tamanho= string.Empty

            });
            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 4,
                Nome = "Nike Purple Run",
                Descricao = "tenis",
                Preco = 199.99m,
                ImagemUrl = "/assets/tenis-nike-downshifter-11-feminino-img.jpg",
                Genero = EGenero.Feminino,
                Tamanho= string.Empty

            });
            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 5,
                Nome = "Tenis Puma Branco",
                Descricao = "tenis",
                Preco = 289.99m,
                ImagemUrl = "/assets/tenisPuma.webp",
                Genero = EGenero.Masculino,
                Tamanho= string.Empty

            });
            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 6,
                Nome = "Tenis Nike Air Jordan",
                Descricao = "tenis",
                Preco = 289.99m,
                ImagemUrl = "/assets/ct0978-060_4.jpg",
                Genero = EGenero.Masculino,
                Tamanho= string.Empty

            }); modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 7,
                Nome = "Tenis Asics Verde",
                Descricao = "tenis",
                Preco = 249.99m,
                ImagemUrl = "/assets/1011B004_300_SR_RT_GLB_PNG_1280x1280-JPG.webp",
                Genero = EGenero.Masculino,
                Tamanho= string.Empty

            }); modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 9,
                Nome = "Tenis Air Max Colorido",
                Descricao = "tenis",
                Preco = 349.99m,
                ImagemUrl = "/assets/MaxColorido.webp",
                Genero = EGenero.Masculino,
                Tamanho= string.Empty

            });

        }

    }
}
