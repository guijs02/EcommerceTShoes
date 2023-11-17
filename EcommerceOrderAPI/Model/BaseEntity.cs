using System.ComponentModel.DataAnnotations;

namespace EcommerceOrderAPI.Model
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
