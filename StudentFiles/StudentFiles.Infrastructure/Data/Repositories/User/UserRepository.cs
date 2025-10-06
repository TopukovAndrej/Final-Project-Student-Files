namespace StudentFiles.Infrastructure.Data.Repositories.User
{
    using Microsoft.EntityFrameworkCore;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Data.Mappers;
    using StudentFiles.Infrastructure.Data.Models;
    using StudentFiles.Infrastructure.Database.Context;

    public class UserRepository : IUserRepository
    {
        private readonly IStudentFilesDbContext _dbContext;

        public UserRepository(IStudentFilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Domain.Entities.User.User>> GetUserByUidAndRoleAsync(Guid userUid, string userRole)
        {
            User? dbUser = await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(predicate: x => !x.IsDeleted
                                                                                                   && x.Uid == userUid
                                                                                                   && x.Role == userRole);

            if (dbUser == null)
            {
                return Result<Domain.Entities.User.User>.Failed(error: new Error(Code: ErrorCodes.UserNotFound, Message: ErrorMessage.UserNotFound),
                                                                resultType: ResultType.NotFound);
            }

            return DataToDomainMapper.MapUserDataToDomain(dbUser: dbUser);
        }

        public async Task<Result<Domain.Entities.User.User>> GetUserByUidAsync(Guid userUid)
        {
            User? dbUser = await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(predicate: x => !x.IsDeleted
                                                                                                   && x.Uid == userUid);

            if (dbUser == null)
            {
                return Result<Domain.Entities.User.User>.Failed(error: new Error(Code: ErrorCodes.UserNotFound, Message: ErrorMessage.UserNotFound),
                                                                resultType: ResultType.NotFound);
            }

            return DataToDomainMapper.MapUserDataToDomain(dbUser: dbUser);
        }

        public async Task InsertUserAsync(Domain.Entities.User.User user)
        {
            User dbUser = DomainToDataMapper.MapUserDomainToData(domainUser: user);

            await _dbContext.Users.AddAsync(entity: dbUser);
        }

        public void UpdateUser(Domain.Entities.User.User user)
        {
            User dbUser = DomainToDataMapper.MapUserDomainToData(domainUser: user);

            _dbContext.Users.Update(dbUser);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfUserExistsByUsernameAsync(string username)
        {
            return await _dbContext.Users.AnyAsync(x => !x.IsDeleted && x.Username == username);
        }
    }
}
