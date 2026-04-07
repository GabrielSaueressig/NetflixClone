using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class LoginDto
    {
        [EmailAddress]
        public string email { get; set; } = string.Empty;
        
        [PasswordPropertyText]
        public string password { get; set; } = string.Empty;
    }
}