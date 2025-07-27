namespace Deskstones.LMS.WebAPI.Controllers
{
    using Deskstones.LMS.WebAPI.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts;
    using Software.DataContracts.Models;

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
            return await _service.UpdateSubjectAsync(request);
        }

        [HttpDelete("{subjectId}")]
        [ProducesResponseType(typeof(DTOGenericResponse), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> DeleteSubjectAsync(int subjectId)
        {
            return await _service.DeleteSubjectAsync(subjectId);
        }
    }
}
