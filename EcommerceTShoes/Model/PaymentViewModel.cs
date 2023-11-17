using EcommerceWeb.Model;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Model
{
    public class PaymentViewModel
    {
        [Required]
        public string Nome { get; set; }
        [StringLength(3)]
        [Required]
        public string Cvv { get; set; }
        [CreditCard]
        public string Numero { get; set; }
        [StringLength(6)]
        [Required]
        public string Validade { get; set; }
    }
}

