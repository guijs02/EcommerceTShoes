namespace EcommerceCartAPI.Domain.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int OrderId { get; set; }
    }
}
