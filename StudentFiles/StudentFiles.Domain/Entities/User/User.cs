namespace StudentFiles.Domain.Entities.User
{
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Domain.Common.Entities;

    public class User : AggregateRoot
    {
        public string Username { get; private set; }

        public string HashedPassword { get; private set; }

        public UserRole Role { get; private set; }

        private User(int id, Guid uid, bool isDeleted, string username, string hashedPassword, UserRole role)
            : base(id: id, uid: uid, isDeleted: isDeleted)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Role = role;
        }

        public static Result<User> Create(int id, Guid uid, bool isDeleted, string username, string hashedPassword, string role)
        {
            Result<UserRole> userRoleResult = UserRole.Create(code: role);

            if (userRoleResult.IsFailure)
            {
                return Result<User>.Failed(error: userRoleResult.Error, resultType: userRoleResult.Type);
            }

            return Result<User>.Success(new User(id: id,
                                                 uid: uid,
                                                 isDeleted: isDeleted,
                                                 username: username,
                                                 hashedPassword: hashedPassword,
                                                 role: userRoleResult.Value!));
        }

        public void MarkDelete()
        {
            IsDeleted = true;
        }
    }
}
