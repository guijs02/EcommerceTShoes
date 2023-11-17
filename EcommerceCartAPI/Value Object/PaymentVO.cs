using System.ComponentModel.DataAnnotations;

namespace EcommerceCartAPI.Value_Object
{
    public class PaymentVO
    {
        public string Nome { get; set; }
        public string Cvv { get; set; }
        public string Numero { get; set; }
        public string Validade { get; set; }
    }
}
