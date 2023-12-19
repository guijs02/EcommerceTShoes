using System.ComponentModel.DataAnnotations;

namespace EcommerceCartAPI.Domain.Models
{
    public class Produto
    {
        [Required]
        public int Id { get; set; }
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
        public EGenero Genero { get; set; }
    }

    public enum EGenero
    {
        Masculino = 1,
        Feminino = 2,
    }
}
