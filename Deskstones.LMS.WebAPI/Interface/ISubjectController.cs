namespace Deskstones.LMS.WebAPI.Interface
{
    using Microsoft.AspNetCore.Mvc;
    using Software.DataContracts.Models;

    public interface ISubjectController
    {
        Task<IActionResult> CreateSubjectAsync(DTOCreateSubjectRequest request);
        Task<IActionResult> GetSubjectAsync(int subjectId);
        Task<IActionResult> UpdateSubjectAsync(DTOUpdateSubjectRequest request);
        Task<IActionResult> DeleteSubjectAsync(int subjectId);
        Task<IActionResult> GetSubjectsAsync(int pageNumber, int pageSize);
    }
}
