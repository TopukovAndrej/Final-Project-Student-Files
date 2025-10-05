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

        public static Grade MapGradeDomainToData(Domain.Entities.Grade.Grade domainGrade)
        {
            return new Grade(id: domainGrade.Id,
                             uid: domainGrade.Uid,
                             isDeleted: domainGrade.IsDeleted,
                             value: domainGrade.Value,
                             dateAssigned: domainGrade.DateAssigned,
                             studentFk: domainGrade.Student.Id,
                             professorFk: domainGrade.Professor.Id,
                             courseFk: domainGrade.Course.Id);
        }
    }
}
