namespace Deskstones.LMS.WebAPI.Interface
{
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts.Models;

    public interface IUserProfileController
    {
        Task<IActionResult> CreateOrUpdateUserProfileAsync(int userId, DTOUserProfileUpdateRequest request);
    }
}
