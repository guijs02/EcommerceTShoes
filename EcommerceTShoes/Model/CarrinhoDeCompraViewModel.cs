namespace EcommerceWeb.Model
{
    public class CarrinhoDeCompraViewModel
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string ImagemUrl { get; set; }
        public string Tamanho { get; set; }
        public int Quantidade { get; set; } = 1;
        public string? UserId { get; set; }
        public string TotalCompraView { get; set; }
        public decimal TotalCompra { get; set; }

        public object ObterTotalCompra(List<CarrinhoDeCompraViewModel> list, bool onlyToView)
        {

            TotalCompra = CalcularValorTotal(list);

            if (!onlyToView)
                return TotalCompra;

            TotalCompraView = TotalCompra.ToString("C2");
            return TotalCompraView;
        }
        private decimal CalcularValorTotal(List<CarrinhoDeCompraViewModel> list)
        {
            //if (!list.Any())
            //    throw new Exception("Não há produtos na lista de carrinho");
            return list.Sum(s => s.Preco * s.Quantidade);
        }

    }
}
