namespace Deskstones.LMS.WebAPI.Util
{
    using System.Security.Claims;

    public static class AppHelper
    {
        public static void CheckAuthorization(ClaimsPrincipal user, int requestedUserId)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "user id")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var jwtUserId))
                throw new UnauthorizedAccessException("Invalid or missing user ID in token.");

            if (jwtUserId != requestedUserId)
                throw new UnauthorizedAccessException("You are not allowed to update another user's profile.");
        }
    }
}
