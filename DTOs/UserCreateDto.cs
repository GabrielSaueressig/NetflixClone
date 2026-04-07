
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 40 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [PasswordPropertyText]
        public string password {get;set;} = string.Empty;
    }

}