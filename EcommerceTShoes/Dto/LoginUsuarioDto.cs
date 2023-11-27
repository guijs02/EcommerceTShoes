using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Dto
{
    public class LoginUsuarioDto
    {
        [Required(ErrorMessage = "Username é obrigatório")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Insira a senha")]
        public string Password { get; set; }

    }
}
