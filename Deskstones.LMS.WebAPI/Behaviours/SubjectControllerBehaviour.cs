namespace Deskstones.LMS.WebAPI.Behaviours
{
    using Deskstones.LMS.BusinessLogic.Interface;
    using Deskstones.LMS.WebAPI.Interface;
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts.Models;

    internal sealed class SubjectControllerBehaviour(ISubjectOrchestrator subjectOrchestrator) : ISubjectController
    {
        public async Task<IActionResult> CreateSubjectAsync(DTOCreateSubjectRequest request)
        {
            var response = await subjectOrchestrator.CreateSubjectAsync(request);
            return new OkObjectResult(response);
        }

        public async Task<IActionResult> GetSubjectAsync(int subjectId)
        {
            var response = await subjectOrchestrator.GetSubjectAsync(subjectId);
            return new OkObjectResult(response);
        }

        public async Task<IActionResult> UpdateSubjectAsync(DTOUpdateSubjectRequest request)
        {
            var response = await subjectOrchestrator.UpdateSubjectAsync(request);
            return new OkObjectResult(response);
        }

        public async Task<IActionResult> DeleteSubjectAsync(int subjectId)
        {
            var response = await subjectOrchestrator.DeleteSubjectAsync(subjectId);
            return new OkObjectResult(response);
        }

        public async Task<IActionResult> GetSubjectsAsync(int pageNumber, int pageSize)
        {
            var response = await subjectOrchestrator.GetSubjectsAsync(pageNumber, pageSize);
            return new OkObjectResult(response);
        }
    }
}
