namespace Deskstones.LMS.Domain.Interface
{
    using Deskstones.LMS.Infrastructure.Models;
    using Software.DataContracts.Models;

    public interface ISubjectRepository
    {
        Task<Subject> CreateSubjectAsync(Subject request);
        Task<Subject> GetSubjectAsync(int subjectId);
        Task<DTOGenericResponse> UpdateSubjectAsync(DTOUpdateSubjectRequest request);
        Task<DTOGenericResponse> DeleteSubjectAsync(int subjectId);
    }
}
