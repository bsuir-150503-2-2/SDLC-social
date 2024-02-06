using razam.Models;

public interface IProfileService
{
    Task<Profile> GetRandomProfileAsync();
    Task<Profile> GetProfileAsync(int profileId);
    Task<bool> LikeProfileAsync(string likerUserId, int likedProfileId);
    Task<bool> RejectProfileAsync(string rejectUserId, int rejectedProfileId);
    Task<List<Profile>> GetAllMatchesAsync(string userId);
}