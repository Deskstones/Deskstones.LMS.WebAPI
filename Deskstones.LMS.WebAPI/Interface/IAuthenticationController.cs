namespace Deskstones.LMS.WebAPI.Interface
{
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts.Models;

    public interface IAuthenticationController
    {
        public Task<IActionResult> LoginAsync(DTOLoginRequest request);

        public Task<IActionResult> RegisterAsync(DTORegisterationRequest request);
    }
}
