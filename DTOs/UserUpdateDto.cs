
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UserUpdateDto
    {

        [EmailAddress]
        public string? Email {set; get;}

        public string? Name {set; get;}

    }
}