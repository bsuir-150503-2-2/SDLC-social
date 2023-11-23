using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using razam.Models;
using razam.Options;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public UserService(UserManager<User> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<string> RegisterUserAsync(LoginRegisterModel model)
    {
        var user = new User { UserName = model.UserName };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var profile = new Profile { Bio = "Default Bio", User = user };
            _context.Add(profile);
            _context.SaveChanges();
            
            return GenerateToken(user);
        }

        return null; // or handle the error as needed
    }

    public async Task<string> LoginUserAsync(LoginRegisterModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            return GenerateToken(user);

        return null; // or handle the error as needed
    }

    public async Task<bool> UpdateProfileAsync(string userId, ProfileUpdateModel model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return false;

        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

        if (profile == null)
            return false;

        profile.Bio = model.Bio;
        _context.SaveChanges();
        return true;
    }

    private string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
