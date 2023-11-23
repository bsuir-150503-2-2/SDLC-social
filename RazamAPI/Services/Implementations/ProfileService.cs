using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using razam.Models;

public class ProfileService : IProfileService
{
    private readonly ApplicationDbContext _context;

    public ProfileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Profile> GetRandomProfileAsync()
    {
        var allProfiles = await _context.Profiles.ToListAsync();

        if (allProfiles.Count == 0)
        {
            return null;
        }

        var random = new Random();
        var randomIndex = random.Next(0, allProfiles.Count);

        return allProfiles[randomIndex];
    }
    
    public async Task<Profile> GetProfileAsync(int profileId)
    {
        var profile = await _context.Profiles
            .FirstOrDefaultAsync(p => p.Id == profileId);

        return profile;
    }


    public async Task<bool> LikeProfileAsync(int profileId)
    {
        // добавляем лайк в бд
        return true;
    }

    public async Task<bool> RejectProfileAsync(int profileId)
    {
        return false;
    }
}


