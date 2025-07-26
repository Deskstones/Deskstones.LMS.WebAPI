namespace Deskstones.LMS.Domain
{
    using Deskstones.LMS.Domain.Interface;
    using Deskstones.LMS.Infrastructure.Data;
    using Deskstones.LMS.Infrastructure.Models;
    using FileManager.Client;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Software.DataContracts.Models;
    using Software.DataContracts.Shared;

    internal sealed class UserProfileRepository(RailwayContext context, IFileUploader fileUploader) : IUserProfileRepository
    {
        public async Task<DTOGenericResponse> CreateOrUpdateUserProfileAsync(int userId, DTOUserProfileUpdateRequest request)
        {
            var userExists = await context.User.AnyAsync(u => u.Id == userId);
            if (!userExists)
            {
                throw new CustomApiException("Invalid user ID. User does not exist.");
            }

            // Check if the profile exists for this user
            var userProfile = await context.UserProfile.Include(p => p.Address).FirstOrDefaultAsync(p => p.UserId == userId);

            if (userProfile == null)
            {
                // Create new profile
                userProfile = new UserProfile
                {
                    UserId = userId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MiddleName = request.MiddleName,
                    Phone = request.Phone,
                    Bio = request.Bio,
                    DateOfBirth = request.DateOfBirth,
                    ProfilePictureUrl = "test.png",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };

                // Handle Address if provided
                if (request.Address != null)
                {
                    userProfile.Address = new UserAddress
                    {
                        Street = request.Address.Street,
                        City = request.Address.City,
                        State = request.Address.State,
                        Country = request.Address.Country,
                        PostalCode = request.Address.PostalCode,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                }

                await context.UserProfile.AddAsync(userProfile);
                await context.SaveChangesAsync();
            }
            else
            {
                // Update existing profile
                userProfile.FirstName = request.FirstName;
                userProfile.LastName = request.LastName;
                userProfile.MiddleName = request.MiddleName;
                userProfile.Phone = request.Phone;
                userProfile.Bio = request.Bio;
                userProfile.DateOfBirth = request.DateOfBirth;
                userProfile.ProfilePictureUrl = (await UploadFileToBucket(userId,request.ProfilePicture!));
                userProfile.UpdatedAt = DateTime.UtcNow;

                if (request.Address != null)
                {
                    if (userProfile.Address == null)
                    {
                        userProfile.Address = new UserAddress
                        {
                            CreatedAt = DateTime.UtcNow
                        };
                    }

                    userProfile.Address.Street = request.Address.Street;
                    userProfile.Address.City = request.Address.City;
                    userProfile.Address.State = request.Address.State;
                    userProfile.Address.Country = request.Address.Country;
                    userProfile.Address.PostalCode = request.Address.PostalCode;
                    userProfile.Address.UpdatedAt = DateTime.UtcNow;
                }
            }

            // Handle social
            var userSocial = await context.UserSocial.FirstOrDefaultAsync(s => s.UserProfileId == userProfile.Id);

            if (request.Social != null)
            {
                if (userSocial == null)
                {
                    userSocial = new UserSocial
                    {
                        UserProfileId = userProfile.Id,
                        LinkedInUrl = request.Social.LinkedInUrl,
                        GitHubUrl = request.Social.GitHubUrl,
                        TwitterUrl = request.Social.TwitterUrl,
                        WebsiteUrl = request.Social.WebsiteUrl,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await context.UserSocial.AddAsync(userSocial);
                }
                else
                {
                    userSocial.LinkedInUrl = request.Social.LinkedInUrl;
                    userSocial.GitHubUrl = request.Social.GitHubUrl;
                    userSocial.TwitterUrl = request.Social.TwitterUrl;
                    userSocial.WebsiteUrl = request.Social.WebsiteUrl;
                    userSocial.UpdatedAt = DateTime.UtcNow;
                }
            }

            await context.SaveChangesAsync();

            return new DTOGenericResponse
            {
                Message = "Success"
            };
        }

        public async Task<DTOGenericResponse> DeleteUserProfileAsync(int userId)
        {
            var userExists = await context.User.AnyAsync(u => u.Id == userId);

            if (!userExists)
            {
                throw new CustomApiException("Invalid user ID. User does not exist.");
            }

            var userProfile = await context.UserProfile.Include(p => p.Address).FirstOrDefaultAsync(p => p.UserId == userId);

            if (userProfile == null)
            {
                throw new CustomApiException("User profile not found.");
            }

            var userSocial = await context.UserSocial.FirstOrDefaultAsync(s => s.UserProfileId == userProfile.Id);
            if (userSocial != null)
            {
                context.UserSocial.Remove(userSocial);
            }

            if (userProfile.Address != null)
            {
                context.UserAddress.Remove(userProfile.Address);
            }

            context.UserProfile.Remove(userProfile);

            await context.SaveChangesAsync();

            return new DTOGenericResponse
            {
                Message = "Success"
            };
        }

        private async Task<string> UploadFileToBucket(int userId, IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return string.Empty;

            var bucketName = "lms-user-profile";
            var extension = Path.GetExtension(photo.FileName)?.ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(extension))
                extension = "";

            var objectName = $"{userId}{extension}";

            using (var stream = photo.OpenReadStream())
            {
                await fileUploader.UploadFileAsync(
                    bucketName,
                    objectName,
                    stream,
                    photo.Length,
                    photo.ContentType
                );
            }

            return objectName;
        }
    }
}
