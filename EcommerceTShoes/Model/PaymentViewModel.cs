using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Model
{
    public class PaymentViewModel
    {
        [Required(ErrorMessage = "Nome do titular obrigatório")]
        public string Nome { get; set; }
        [StringLength(3, ErrorMessage = "Maximo de 3 caracteres")]
        [Required(ErrorMessage = "CVV obrigatório")]
        public string Cvv { get; set; }
        [Required(ErrorMessage ="O número do cartão é obrigatório")]
        //[CreditCard(ErrorMessage = "Cartão de crédito invalido")]
        public string Numero { get; set; }
        [StringLength(6,ErrorMessage = "Maximo de 6 caracteres")]
        [Required(ErrorMessage = "Validade obrigatória")]
        public string Validade { get; set; }
    }
}

