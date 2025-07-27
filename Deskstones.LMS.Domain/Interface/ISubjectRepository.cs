namespace Deskstones.LMS.Domain.Interface
{
    using Deskstones.LMS.Infrastructure.Models;
    using Software.DataContracts.Models;
    using Software.DataContracts.Shared;

    public interface ISubjectRepository
    {
        Task<CourseSubject> CreateSubjectAsync(CourseSubject request);
        Task<CourseSubject> GetSubjectAsync(int subjectId);
        Task<DTOGenericResponse> UpdateSubjectAsync(DTOUpdateSubjectRequest request);
        Task<DTOGenericResponse> DeleteSubjectAsync(int subjectId);
        Task<DTOPaginatedList<DTOSubjectResponse>> GetSubjectsAsync(int pageNumber, int pageSize);
    }
}
