using Deskstones.LMS.Domain.Interface;
using Deskstones.LMS.Infrastructure.Data;
using Deskstones.LMS.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Software.DataContracts.Models;
using Software.DataContracts.Shared;


namespace Deskstones.LMS.Domain
{
    internal sealed class SubjectRepository(RailwayContext context) : ISubjectRepository
    {
        public async Task<CourseSubject> GetSubjectAsync(int subjectId)
        {
            var repsonse = await context.CourseSubject.FindAsync(subjectId);
            if (repsonse == null)
            {
                throw new CustomApiException("subject not found");
            }
            return repsonse;
        }

        public async Task<CourseSubject> CreateSubjectAsync(CourseSubject request)
        {
            context.CourseSubject.Add(request);
            await context.SaveChangesAsync();
            return request;
        }

        public async Task<DTOGenericResponse> UpdateSubjectAsync(DTOUpdateSubjectRequest request)
        {
            var subject = await context.CourseSubject.FindAsync(request.Id);

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

            context.CourseSubject.Update(subject);

            await context.SaveChangesAsync();

            return new DTOGenericResponse
            {
                Id = request.Id,
                Message = "subject successfully updated"
            };
        }

        public async Task<DTOGenericResponse> DeleteSubjectAsync(int subjectId)
        {
            var subject = await context.CourseSubject.FindAsync(subjectId);
            if (subject == null)
            {
                throw new CustomApiException("subject not found");
            }

            context.CourseSubject.Remove(subject);
            await context.SaveChangesAsync();

            return new DTOGenericResponse 
            { 
                Id = subjectId,
                Message = "subject successfully removed"
            };
        }

        public async Task<DTOPaginatedList<DTOSubjectResponse>> GetSubjectsAsync(int pageNumber, int pageSize)
        {
            var query = context.CourseSubject.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(s => s.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new DTOSubjectResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Code = s.Code,
                    DurationInMonths = s.DurationInMonths,
                    Cost = s.Cost
                })
                .ToListAsync();

            return new DTOPaginatedList<DTOSubjectResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
