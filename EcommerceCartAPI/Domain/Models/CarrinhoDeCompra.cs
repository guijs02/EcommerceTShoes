using System.ComponentModel.DataAnnotations;

namespace EcommerceCartAPI.Domain.Models
{
    public class CarrinhoDeCompra
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string ImagemUrl { get; set; }
        [Required]
        public string Tamanho { get; set; }
        [Required]
        public int Quantidade { get; set; } = 1;
        
        public string? UserId { get; set; }
    }
}
