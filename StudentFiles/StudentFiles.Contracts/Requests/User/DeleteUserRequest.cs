namespace StudentFiles.Contracts.Requests.User
{
    public class DeleteUserRequest
    {
        public Guid UserUid { get; }

        public DeleteUserRequest(Guid userUid)
        {
            UserUid = userUid;
        }
    }
}
