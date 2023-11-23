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

    [HttpPost("{profileId}/like")]
    public async Task<ActionResult> LikeProfile(int profileId)
    {
        
        await _profileService.LikeProfileAsync(profileId);
        return Ok();
    }

    [HttpPost("{profileId}/reject")]
    public async Task<ActionResult> RejectProfile(int profileId)
    {
        await _profileService.RejectProfileAsync(profileId);
        return Ok();
    }
}