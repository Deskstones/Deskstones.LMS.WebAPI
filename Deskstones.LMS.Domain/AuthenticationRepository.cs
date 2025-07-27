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

            var user = await context.AppUser.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                var isCorrectPassword = PasswordManager.VerifyPassword(user.PasswordHash, password);

                if (isCorrectPassword)
                {
                    var userRegisterationDate = user.CreatedAt.ToString("dd/MM/yyy HH:mm:ss");
                    var userRole = user.Role;

                    var token = tokenService.GenerateToken(user.Id.ToString(), user.Email, userRole, userRegisterationDate);

                    var response = new DTOLoginResponse
                    {
                        Token = token
                    };

                    return response;
                }
            }
            throw new CustomApiException("Invalid username or password");
        }

        public async Task<DTOGenericResponse> RegisterAsync(DTORegisterationRequest request)
        {
            var existingUser = await context.AppUser.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower());
            if (existingUser != null)
            {
                throw new CustomApiException("This email already exists");
            }

            var newUser = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                Role = "Student",
                PasswordHash = PasswordManager.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.AppUser.Add(newUser);
            await context.SaveChangesAsync();

            var response = new DTOGenericResponse
            {
                Id = newUser.Id,
                Message = "Success"
            };
            
             return response;
        }
    }
}
