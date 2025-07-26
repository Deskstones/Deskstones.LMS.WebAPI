namespace Deskstones.LMS.WebAPI.Controllers
{
    using Deskstones.LMS.WebAPI.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts;
    using Software.DataContracts.Models;

    [ApiController]
    [Route("authenticate")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationController _service;
        public AuthenticationController(IAuthenticationController service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DTOLoginResponse), StatusCodes.Status200OK)]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] DTOLoginRequest request)
        {
            return await this._service.LoginAsync(request);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DTOGenericResponse), StatusCodes.Status200OK)]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] DTORegisterationRequest request)
        {
            return await this._service.RegisterAsync(request);
        }
    }
}
