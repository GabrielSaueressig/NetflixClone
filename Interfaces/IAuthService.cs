using DTOs;

public interface IAuthService
{
    public Task<string?> LoginAsync(LoginDto loginDto);
}