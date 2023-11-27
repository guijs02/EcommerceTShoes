using System.ComponentModel.DataAnnotations;

namespace LoginAPI.Dto
{
    public class CreateUsuarioDto
    {
        [Required(ErrorMessage = "Username é obrigatório")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "digite a senha novamente")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string PasswordConfirmation { get; set; }
    }
}
