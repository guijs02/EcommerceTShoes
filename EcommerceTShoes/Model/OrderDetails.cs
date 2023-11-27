namespace EcommerceWeb.Model
{
    public class OrderDetails
    {
        public OrderDetails()
        {
            Payment = new();
            Cart = new();
            OrderSummary = new();
        }
        public PaymentViewModel Payment { get; set; }
        public List<CarrinhoDeCompraViewModel> Cart { get; set; }
        public OrderSummary OrderSummary { get; set; }
    }
    public class OrderSummary
    {
        public string Frete { get; set; } = "Gratis";
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
