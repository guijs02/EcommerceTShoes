namespace EcommerceCartAPI.Models
{
    public class ProdutoCarrinhoDto
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public string Tamanho { get; set; }
        //public decimal ValorTotal { get; set; }
    }
}
