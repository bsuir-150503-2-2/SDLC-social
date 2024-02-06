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
            return null;

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


    public async Task<bool> LikeProfileAsync(string likerUserId, int likedProfileId)
    {
        var likedProfile = await _context.Profiles.FindAsync(likedProfileId);

        if (likedProfile != null && likedProfile.UserId != likerUserId)
        {
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l => l.LikerId == likerUserId && l.LikedId == likedProfile.UserId);

            if (existingLike == null)
            {
                var like = new Like
                {
                    LikerId = likerUserId,
                    LikedId = likedProfile.UserId,
                    LikedAt = DateTime.UtcNow
                };

                _context.Likes.Add(like);
                await _context.SaveChangesAsync();

                return true;
            }
        }

        return false;
    }

    public async Task<bool> RejectProfileAsync(string rejecterUserId, int rejectedProfileId)
    {
        var rejectedProfile = await _context.Profiles.FindAsync(rejectedProfileId);

        if (rejectedProfile != null)
        {
            var existingLike = await _context.Likes
                .Where(l => l.LikerId == rejectedProfile.UserId && l.LikedId == rejecterUserId)
                .FirstOrDefaultAsync();

            if (existingLike != null)
            {
                _context.Likes.Remove(existingLike);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        return true;
    }

    public async Task<List<Profile>> GetAllMatchesAsync(string userId)
    {
        var matches = await _context.Likes
            .Where(l => l.LikerId == userId)
            .Join(
                _context.Likes,
                like => new { UserId = like.LikedId, LikerId = userId },
                reverseLike => new { UserId = reverseLike.LikerId, LikerId = reverseLike.LikedId },
                (like, reverseLike) => new { Like = like, ReverseLike = reverseLike }
            )
            .Select(match => match.Like.LikedId)
            .ToListAsync();

        var matchProfiles = await _context.Profiles
            .Where(profile => matches.Contains(profile.UserId))
            .ToListAsync();

        return matchProfiles;
    }


}