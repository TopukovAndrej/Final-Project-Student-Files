namespace StudentFiles.Infrastructure.Data.Mappers
{
    using StudentFiles.Infrastructure.Data.Models;

    public static class DomainToDataMapper
    {
        public static User MapUserDomainToData(Domain.Entities.User.User domainUser)
        {
            return new User(id: domainUser.Id,
                            uid: domainUser.Uid,
                            isDeleted: domainUser.IsDeleted,
                            username: domainUser.Username,
                            hashedPassword: domainUser.HashedPassword,
                            role: domainUser.Role.Code);
        }
    }
}
