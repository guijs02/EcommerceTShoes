using EcommerceWeb.Pages.Carrinho;

namespace EcommerceWeb.Model
{
    public class OrderDetails
    {
        public PaymentViewModel Payment { get; set; }
        public List<CarrinhoDeCompraViewModel> Cart { get; set; }
    }
}
