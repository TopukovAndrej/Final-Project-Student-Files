namespace StudentFiles.Infrastructure.Data.Repositories.Course
{
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Common.Interfaces;
    using StudentFiles.Infrastructure.Data.Models;

    public interface ICourseRepository : IRepository<Course>
    {
        public Task<Result<Domain.Entities.Course.Course>> GetCourseByUidAsync(Guid uid);

        public Task<bool> CheckIfProfessorHasCoursesAsync(int professorId);
    }
}
