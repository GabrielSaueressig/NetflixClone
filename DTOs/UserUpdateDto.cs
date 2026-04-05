
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UserUpdateDto
    {

        [EmailAddress]
        public string? Email {set; get;}

        [StringLength(40,MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 40 caracteres")]
        public string? Name {set; get;}

    }
}