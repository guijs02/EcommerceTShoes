namespace EcommerceTShoes.Model
{
    public class CarrinhoDeCompra
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public string Tamanho { get; set; }

    }
}
