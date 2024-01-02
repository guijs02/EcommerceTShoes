namespace EcommerceCartAPI.Domain.Models
{
    public class ProdutoCarrinhoDto
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public string Tamanho { get; set; }

        public static implicit operator ProdutoCarrinhoDto(CarrinhoDeCompra carrinhoDeCompra)
        {
            if (carrinhoDeCompra is null)
            {
                return null;
            }

            return new ProdutoCarrinhoDto()
            {
                Id = carrinhoDeCompra.ProdutoId,
                Descricao = carrinhoDeCompra.Descricao,
                ImagemUrl = carrinhoDeCompra.ImagemUrl,
                Nome = carrinhoDeCompra.Nome,
                Preco = carrinhoDeCompra.Preco,
                Tamanho = carrinhoDeCompra.Tamanho,
            };
        }
    }
}
