
namespace DTOs
{
    public class UserReadDto
    {
        public int Id { get; set; }            // Id normalmente exposto para identificar o recurso
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}