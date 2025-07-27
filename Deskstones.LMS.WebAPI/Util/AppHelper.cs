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
                throw new UnauthorizedAccessException("You are not allowed to perform this action.");
        }

        public static void CheckAdminAuthorization(ClaimsPrincipal user)
        {
            var userRole = user.Claims.FirstOrDefault(c => c.Type == "user role")?.Value;

            if (string.IsNullOrEmpty(userRole))
                throw new UnauthorizedAccessException("Invalid or missing user role in token.");

            if (userRole != "Admin")
                throw new UnauthorizedAccessException("You are not an admin.");
        }
    }
}
