namespace Deskstones.LMS.BusinessLogic
{
    using Deskstones.LMS.BusinessLogic.Interface;
    using Deskstones.LMS.Domain.Interface;
    using Deskstones.LMS.Infrastructure.Models;
    using Software.DataContracts.Models;

    internal class SubjectOrchestrator(ISubjectRepository subjectRepository) : ISubjectOrchestrator
    {
        public async Task<DTOSubjectResponse> CreateSubjectAsync(DTOCreateSubjectRequest request)
        {
            var repoRequest = new Subject
            {
                Code = request.Code,
                Cost = request.Cost,
                DurationInMonths = request.DurationInMonths,
                Description = request.Description,
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var response = await subjectRepository.CreateSubjectAsync(repoRequest);

            return new DTOSubjectResponse
            {
                Id = response.Id,
                Name = response.Name,
                Code = response.Code,
                Cost = response.Cost,
                DurationInMonths = response.DurationInMonths,
                Description = response.Description,
                UpdatedAt = response.UpdatedAt,
                CreatedAt = response.CreatedAt,
            };
        }

        public async Task<DTOSubjectResponse> GetSubjectAsync(int subjectId)
        {
            var response = await subjectRepository.GetSubjectAsync(subjectId);

            return new DTOSubjectResponse
            {
                Id = response.Id,
                Name = response.Name,
                Code = response.Code,
                Cost = response.Cost,
                DurationInMonths = response.DurationInMonths,
                Description = response.Description,
                UpdatedAt = response.UpdatedAt,
                CreatedAt = response.CreatedAt,
            };
        }

        public async Task<DTOGenericResponse> UpdateSubjectAsync(DTOUpdateSubjectRequest request)
        {
           return await subjectRepository.UpdateSubjectAsync(request);

        }

        public async Task<DTOGenericResponse> DeleteSubjectAsync(int subjectId)
        {
            return await subjectRepository.DeleteSubjectAsync(subjectId);

        }
    }
}
