namespace Deskstones.LMS.WebAPI.Behaviours
{
    using Deskstones.LMS.WebAPI.Interface;
    using Deskstones.LMS.BusinessLogic.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts.Models;

    internal sealed class AuthenticationControllerBehaviour(IAuthenticationOrchestrator authenticationOrchestrator) : IAuthenticationController
    {
        public async Task<IActionResult> LoginAsync(DTOLoginRequest request)
        {
            var reponse = await authenticationOrchestrator.LoginAsync(request);
            return new OkObjectResult(reponse);
        }

        public async Task<IActionResult> RegisterAsync(DTORegisterationRequest request)
        {
            var reponse = await authenticationOrchestrator.RegisterAsync(request);
            return new OkObjectResult(reponse);
        }
    }
}
