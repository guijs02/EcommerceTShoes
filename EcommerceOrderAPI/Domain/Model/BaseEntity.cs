using System.ComponentModel.DataAnnotations;

namespace EcommerceOrderAPI.Domain.Model
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
