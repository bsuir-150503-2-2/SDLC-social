using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using razam.Models;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("random")]
    public async Task<ActionResult<Profile>> GetRandomProfileAsync()
    {
        var randomProfile = await _profileService.GetRandomProfileAsync();

        if (randomProfile == null)
            return NoContent(); // 204 No Content

        return Ok(randomProfile);
    }

    [HttpGet("{profileId}")]
    public async Task<ActionResult<Profile>> GetProfileAsync(int profileId)
    {
        var profile = await _profileService.GetProfileAsync(profileId);

        if (profile == null)
            return NoContent(); // 204 No Content

        return Ok(profile);
    }

    [HttpPut("{profileId}/like")]
    [Authorize]
    public async Task<ActionResult> LikeProfile(int profileId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var success = await _profileService.LikeProfileAsync(userId, profileId);
        if (success)
            return Ok(new { Message = "Profile liked successfully" });

        return NoContent();
    }

    [HttpPut("{profileId}/reject")]
    [Authorize]
    public async Task<ActionResult> RejectProfile(int profileId)
    {
        var rejecterId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var success = await _profileService.RejectProfileAsync(rejecterId, profileId);
        if (success)
            return Ok(new { Message = "Profile rejected successfully" });

        return NoContent();
    }
    
    [HttpGet("matches")]
    [Authorize]
    public async Task<ActionResult<List<Profile>>> GetAllMatches()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    
        if (userId == null)
            return Unauthorized(new { Message = "User not authorized." });

        var matches = await _profileService.GetAllMatchesAsync(userId);

        return Ok(matches);
    }
}