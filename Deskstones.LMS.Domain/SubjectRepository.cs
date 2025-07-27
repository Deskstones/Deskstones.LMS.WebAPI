using Deskstones.LMS.Domain.Interface;
using Deskstones.LMS.Infrastructure.Data;
using Deskstones.LMS.Infrastructure.Models;
using Software.DataContracts.Models;
using Software.DataContracts.Shared;


namespace Deskstones.LMS.Domain
{
    internal sealed class SubjectRepository(RailwayContext context) : ISubjectRepository
    {
        public async Task<Subject> GetSubjectAsync(int subjectId)
        {
            var repsonse = await context.Subject.FindAsync(subjectId);
            if (repsonse == null)
            {
                throw new CustomApiException("subject not found");
            }
            return repsonse;
        }

        public async Task<Subject> CreateSubjectAsync(Subject request)
        {
            context.Subject.Add(request);
            await context.SaveChangesAsync();
            return request;
        }

        public async Task<DTOGenericResponse> UpdateSubjectAsync(DTOUpdateSubjectRequest request)
        {
            var subject = await context.Subject.FindAsync(request.Id);

            if (subject == null)
            {
                throw new CustomApiException("Subject not found");
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
                subject.Name = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Description))
                subject.Description = request.Description;

            if (!string.IsNullOrWhiteSpace(request.Code))
                subject.Code = request.Code;

            if (request.DurationInMonths.HasValue)
                subject.DurationInMonths = request.DurationInMonths.Value;

            if (request.Cost.HasValue)
                subject.Cost = request.Cost.Value;

            subject.UpdatedAt = DateTime.UtcNow;

            context.Subject.Update(subject);

            await context.SaveChangesAsync();

            return new DTOGenericResponse
            {
                Id = request.Id,
                Message = "subject successfully updated"
            };
        }

        public async Task<DTOGenericResponse> DeleteSubjectAsync(int subjectId)
        {
            var subject = await context.Subject.FindAsync(subjectId);
            if (subject == null)
            {
                throw new CustomApiException("subject not found");
            }

            context.Subject.Remove(subject);
            await context.SaveChangesAsync();

            return new DTOGenericResponse 
            { 
                Id = subjectId,
                Message = "subject successfully removed"
            };
        }
    }
}
