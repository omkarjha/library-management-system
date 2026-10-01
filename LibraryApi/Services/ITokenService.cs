using LibraryApi.Models;

namespace LibraryApi.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}