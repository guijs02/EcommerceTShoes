using System.ComponentModel.DataAnnotations;

namespace EcommerceTShoes.Model
{
    public class UserAdress
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Adress { get; set; } 
        public string Email { get; set; }
        [MaxLength(9)]
        public string Phone { get; set; }
        public string AdicionalInfo { get; set; }
    }
}
