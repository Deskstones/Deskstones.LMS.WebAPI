namespace Deskstones.LMS.Domain.Interface
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userEmail);
    }
}
