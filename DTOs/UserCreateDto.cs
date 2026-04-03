
using System.ComponentModel.DataAnnotations;

namespace DTOs{
public class UserCreateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;
}

}