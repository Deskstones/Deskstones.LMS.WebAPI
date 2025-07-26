namespace Deskstones.LMS.WebAPI.Controllers
{
    using Deskstones.LMS.WebAPI.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts;
    using Software.DataContracts.Models;

    [ApiController]
    [Route("user-profile")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileController _service;
        public UserProfileController(IUserProfileController service)
        {
            _service = service;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(DTOGenericResponse), StatusCodes.Status200OK)]
        [Route("update")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateOrUpdateUserProfileAsync([FromQuery] int userId, [FromForm] DTOUserProfileUpdateRequest request)
        {
            return await this._service.CreateOrUpdateUserProfileAsync(userId,request);
        }

 
    }
}
