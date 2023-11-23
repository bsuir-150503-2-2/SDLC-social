using razam.Models;

public interface IUserService
{
    Task<string> RegisterUserAsync(LoginRegisterModel model);
    Task<string> LoginUserAsync(LoginRegisterModel model);
    Task<bool> UpdateProfileAsync(string userId, ProfileUpdateModel model);
}