using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace EcommerceWeb.Dto
{
    public class ProdutoCarrinhoDto
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        [Required(ErrorMessage = "Não é possivel salvar no carrinho sem colocar o tamanho")]
        public string Tamanho { get; set; }
        //public decimal ValorTotal { get; set; }
    }
}
