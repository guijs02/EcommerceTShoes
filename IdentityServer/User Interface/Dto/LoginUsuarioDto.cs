using System.ComponentModel.DataAnnotations;

namespace IdentityServer
{
    public class LoginUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
