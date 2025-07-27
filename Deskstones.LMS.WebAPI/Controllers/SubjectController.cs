namespace Deskstones.LMS.WebAPI.Controllers
{
    using Deskstones.LMS.Infrastructure.Models;
    using Deskstones.LMS.WebAPI.Interface;
    using Deskstones.LMS.WebAPI.Util;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts;
    using Software.DataContracts.Models;
    using Software.DataContracts.Shared;

    [ApiController]
    [Route("subject")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectController _service;

        public SubjectController(ISubjectController service)
        {
            _service = service;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(DTOGenericResponse), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> CreateSubjectAsync([FromBody] DTOCreateSubjectRequest request)
        {
            AppHelper.CheckAdminAuthorization(User);
            return await _service.CreateSubjectAsync(request);
        }

        [HttpGet("{subjectId}")]
        [ProducesResponseType(typeof(DTOSubjectResponse), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetSubjectAsync(int subjectId)
        {
            return await _service.GetSubjectAsync(subjectId);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(DTOGenericResponse), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> UpdateSubjectAsync([FromBody] DTOUpdateSubjectRequest request)
        {
            AppHelper.CheckAdminAuthorization(User);
            return await _service.UpdateSubjectAsync(request);
        }

        [HttpDelete("{subjectId}")]
        [ProducesResponseType(typeof(DTOGenericResponse), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> DeleteSubjectAsync(int subjectId)
        {
            AppHelper.CheckAdminAuthorization(User);
            return await _service.DeleteSubjectAsync(subjectId);
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(DTOPaginatedList<DTOSubjectResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubjectsAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _service.GetSubjectsAsync(pageNumber, pageSize);
        }

    }
}
