using System.ComponentModel.DataAnnotations;

namespace EcommerceCartAPI.Value_Object
{
    public class PaymentVO
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cvv { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Validade { get; set; }
    }
}
