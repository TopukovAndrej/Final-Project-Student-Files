namespace StudentFiles.Domain.Entities.User
{
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
    }
}
