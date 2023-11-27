namespace EcommerceWeb.Model
{
    public class CarrinhoDeCompraViewModel
    {
        public int Id { get; set; }
        //public string UserId { get; set; }
        public int ProdutoId { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public string Tamanho { get; set; }
        public int Quantidade { get; set; } = 1;
        public string? UserId { get; set; }
    }
}
