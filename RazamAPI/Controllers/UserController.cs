using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using razam.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userProfileService;

    public UserController(IUserService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRegisterModel model)
    {
        var token = await _userProfileService.RegisterUserAsync(model);
        if (token != null)
            return Ok(new { Message = "User registered successfully.", Token = token });

        return BadRequest(new { Message = "Registration failed." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRegisterModel model)
    {
        var token = await _userProfileService.LoginUserAsync(model);
        if (token != null)
            return Ok(new { Message = "Login successful.", Token = token });

        return Unauthorized(new { Message = "Invalid username or password." });
    }

    [HttpPost("update")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(ProfileUpdateModel model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var success = await _userProfileService.UpdateProfileAsync(userId, model);

        if (success)
            return Ok(new { Message = "Profile updated successfully." });

        return NotFound(new { Message = "User or profile not found." });
    }
}