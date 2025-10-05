namespace StudentFiles.Infrastructure.Data.Repositories.Course
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Data.Mappers;
    using StudentFiles.Infrastructure.Data.Models;
    using StudentFiles.Infrastructure.Database.Context;

    public class CourseRepository : ICourseRepository
    {
        private readonly IStudentFilesDbContext _dbContext;

        public CourseRepository(IStudentFilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Domain.Entities.Course.Course>> GetCourseByUidAsync(Guid uid)
        {
            Course? dbCourse = await _dbContext.Courses.Include(x => x.Professor).SingleOrDefaultAsync(x => !x.IsDeleted && x.Uid == uid);

            if (dbCourse is null)
            {
                return Result<Domain.Entities.Course.Course>.Failed(error: new Error(Code: ErrorCodes.CourseNotFound, Message: ErrorMessage.CourseNotFound),
                                                                    resultType: ResultType.NotFound);
            }

            return DataToDomainMapper.MapCourseDataToDomain(dbCourse: dbCourse);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
