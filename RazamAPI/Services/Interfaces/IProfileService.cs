using razam.Models;

public interface IProfileService
{
    Task<Profile> GetRandomProfileAsync();
    Task<Profile> GetProfileAsync(int profileId);
    Task<bool> LikeProfileAsync(int profileId);
    Task<bool> RejectProfileAsync(int profileId);
}