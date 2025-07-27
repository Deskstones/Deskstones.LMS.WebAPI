namespace Deskstones.LMS.WebAPI.Behaviours
{
    using Deskstones.LMS.WebAPI.Interface;
    using Deskstones.LMS.BusinessLogic.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts.Models;

    internal sealed class UserProfileControllerBehaviour(IUserProfileOrchestrator uerProfileOrchestrator) : IUserProfileController
    {
        public async Task<IActionResult> CreateOrUpdateUserProfileAsync(DTOUserProfileUpdateRequest request)
        {
            var reponse = await uerProfileOrchestrator.CreateOrUpdateUserProfileAsync(request);
            return new OkObjectResult(reponse);
        }
    }
}
