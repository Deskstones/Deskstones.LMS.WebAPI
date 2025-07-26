namespace Deskstones.LMS.Domain
{
    using Deskstones.LMS.Domain.Interface;
    using Deskstones.LMS.Infrastructure.Data;
    using Deskstones.LMS.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;
    using Software.DataContracts.Models;
    using Software.DataContracts.Shared;
    using Software.Helper.Util;

    internal sealed class AuthenticationRepository(RailwayContext context, ITokenService tokenService) : IAuthenticationRepository
    {
        public async Task<DTOLoginResponse> LoginAsync(DTOLoginRequest request)
        {
            var email = request.Email;
            var password = request.Password;

            var user = await context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                if (PasswordManager.VerifyPassword(user.PasswordHash, password))
                {
                    var token = tokenService.GenerateToken(user.Id.ToString(), user.Email);

                    var response = new DTOLoginResponse
                    {
                        Token = token,
                        UserName = user.UserName,
                        UserId = user.Id,
                        UpdatedAt = user.UpdatedAt,
                        CreatedAt = user.CreatedAt,
                    };

                    return response;
                }
            }
            throw new CustomApiException("Invalid username or password");
        }

        public async Task<DTOGenericResponse> RegisterAsync(DTORegisterationRequest request)
        {
            var existingUser = await context.User.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
            if (existingUser != null)
            {
                throw new CustomApiException("This email already exists");
            }

            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = PasswordManager.HashPassword(request.Password),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            context.User.Add(newUser);
            await context.SaveChangesAsync();

            var response = new DTOGenericResponse
            {
                Message = "Success"
            };
            
             return response;
        }
    }
}
