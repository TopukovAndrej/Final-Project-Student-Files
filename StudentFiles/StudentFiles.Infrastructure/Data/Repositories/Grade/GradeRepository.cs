namespace StudentFiles.Infrastructure.Data.Repositories.Grade
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Infrastructure.Data.Mappers;
    using StudentFiles.Infrastructure.Data.Models;
    using StudentFiles.Infrastructure.Database.Context;

    public class GradeRepository : IGradeRepository
    {
        private readonly IStudentFilesDbContext _dbContext;

        public GradeRepository(IStudentFilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckIfStudentAlreadyHasGradeForCourseAsync(int studentId, int courseId)
        {
            return await _dbContext.Grades.AnyAsync(x => !x.IsDeleted && x.StudentFk == studentId && x.CourseFk == courseId);
        }

        public async Task<bool> CheckIfStudentIsGraduatedAsync(int studentId)
        {
            return await _dbContext.Grades.CountAsync(x => !x.IsDeleted && x.StudentFk == studentId) == 20;
        }

        public async Task InsertGradeAsync(Domain.Entities.Grade.Grade grade)
        {
            Grade dbGrade = DomainToDataMapper.MapGradeDomainToData(domainGrade: grade);

            await _dbContext.Grades.AddAsync(entity: dbGrade);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
