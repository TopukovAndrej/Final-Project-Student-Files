namespace StudentFiles.Infrastructure.Data.Repositories.Grade
{
    using StudentFiles.Infrastructure.Common.Interfaces;
    using StudentFiles.Infrastructure.Data.Models;

    public interface IGradeRepository : IRepository<Grade>
    {
        public Task<bool> CheckIfStudentAlreadyHasGradeForCourseAsync(int studentId, int courseId);

        public Task<bool> CheckIfStudentIsGraduatedAsync(int studentId);

        public Task InsertGradeAsync(Domain.Entities.Grade.Grade grade);
    }
}
