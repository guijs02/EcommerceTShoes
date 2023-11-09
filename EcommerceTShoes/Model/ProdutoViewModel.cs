using EcommerceWeb.Dto;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Model
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        [Required(ErrorMessage = "Não é possivel salvar no carrinho sem colocar o tamanho")]
        public string Tamanho { get; set; }
        public EGenero Genero { get; set; }

        public static implicit operator ProdutoViewModel(ProdutoCarrinhoDto produtoCarrinho)
                            => new ProdutoViewModel()
                            {
                                Id = produtoCarrinho.Id,
                                Tamanho = produtoCarrinho.Tamanho,
                                Descricao = produtoCarrinho.Descricao,
                                Preco = produtoCarrinho.Preco,
                                Nome = produtoCarrinho.Nome,
                                ImagemUrl = produtoCarrinho.ImagemUrl,
                            };
    }

    public enum EGenero
    {
        Masculino = 1,
        Feminino = 2,
    }
}
