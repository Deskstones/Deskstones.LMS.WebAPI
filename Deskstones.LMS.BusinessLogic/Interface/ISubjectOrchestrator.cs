using Software.DataContracts.Models;
using Software.DataContracts.Shared;

namespace Deskstones.LMS.BusinessLogic.Interface
{
    public interface ISubjectOrchestrator
    {
        Task<DTOSubjectResponse> CreateSubjectAsync(DTOCreateSubjectRequest request);
        Task<DTOSubjectResponse> GetSubjectAsync(int subjectId);
        Task<DTOGenericResponse> UpdateSubjectAsync(DTOUpdateSubjectRequest request);
        Task<DTOGenericResponse> DeleteSubjectAsync(int subjectId);
        Task<DTOPaginatedList<DTOSubjectResponse>> GetSubjectsAsync(int pageNumber, int pageSize);
    }
}
