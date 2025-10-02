namespace StudentFiles.Infrastructure.Data.Mappers
{
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Infrastructure.Data.Models;

    public class DataToDomainMapper
    {
        public static Result<Domain.Entities.User.User> MapUserDataToDomain(User dbUser)
        {
            Result<Domain.Entities.User.User> domainUserResult = Domain.Entities.User.User.Create(id: dbUser.Id,
                                                                                                  uid: dbUser.Uid,
                                                                                                  isDeleted: dbUser.IsDeleted,
                                                                                                  username: dbUser.Username,
                                                                                                  hashedPassword: dbUser.HashedPassword,
                                                                                                  role: dbUser.Role);

            return domainUserResult;
        }
    }
}
