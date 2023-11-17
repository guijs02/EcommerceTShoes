namespace EcommerceOrderAPI.Model
{
    public class Order : BaseEntity
    {
        public string NomeTitular { get; set; }
        public string Cvv { get; set; }
        public string Numero { get; set; }
        public string Validade { get; set; }
        public DateTime DateTime { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}
