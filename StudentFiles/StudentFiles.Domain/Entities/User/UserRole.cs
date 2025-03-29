namespace StudentFiles.Domain.Entities.User
{
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Domain.Common.Models;

    public class UserRole(byte id, string code) : Enumeration(id: id, code: code)
    {
        public static readonly UserRole Admin = new(id: 1, code: "ADMIN");

        public static readonly UserRole Professor = new(id: 2, code: "PROFESSOR");

        public static readonly UserRole Student = new(id: 3, code: "STUDENT");

        public static Result<UserRole> Create(string code)
        {
            UserRole? role = GetAll<UserRole>().SingleOrDefault(predicate: x => x.Code == code);

            if (role is null)
            {
                return Result<UserRole>.Failed(errorMessage: ErrorCodes.UserRoleNotValid, resultType: ResultType.Invalid);
            }

            return Result<UserRole>.Success(value: role);
        }
    }
}
