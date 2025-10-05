namespace StudentFiles.Infrastructure.Data.Repositories.Grade
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Models.Result;
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

        public void UpdateGrade(Domain.Entities.Grade.Grade domainGrade)
        {
            Grade dbGrade = DomainToDataMapper.MapGradeDomainToData(domainGrade: domainGrade);

            _dbContext.Grades.Update(dbGrade);
        }

        public async Task<Result<IReadOnlyList<Domain.Entities.Grade.Grade>>> GetAllGradesForStudentIdAsync(int studentId)
        {
           List<Grade> studentGrades = await _dbContext.Grades.AsNoTracking()
                                                              .Include(x => x.Course)
                                                              .Include(x => x.Professor)
                                                              .Include(x => x.Student)
                                                              .Where(x => !x.IsDeleted && x.StudentFk == studentId)
                                                              .ToListAsync();

            List<Domain.Entities.Grade.Grade> domainStudentGrades = [];

            foreach(Grade grade in studentGrades)
            {
                Result<Domain.Entities.Grade.Grade> domainGradeResult = DataToDomainMapper.MapGradeDataToDomain(grade);

                if (domainGradeResult.IsFailure)
                {
                    return Result<IReadOnlyList<Domain.Entities.Grade.Grade>>.Failed(error: domainGradeResult.Error, resultType: domainGradeResult.Type);
                }

                domainStudentGrades.Add(domainGradeResult.Value!);
            }

            return Result<IReadOnlyList<Domain.Entities.Grade.Grade>>.Success(value: domainStudentGrades);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
