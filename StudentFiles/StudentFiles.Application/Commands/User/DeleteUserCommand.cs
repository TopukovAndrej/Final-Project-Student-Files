namespace StudentFiles.Application.Commands.User
{
    using MediatR;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Contracts.Requests.User;
    using StudentFiles.Domain.Entities.User;
    using StudentFiles.Infrastructure.Data.Repositories.User;

    public class DeleteUserCommand : IRequest<Result>
    {
        public DeleteUserRequest Request { get; }

        public DeleteUserCommand(DeleteUserRequest request)
        {
            Request = request;
        }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken = default)
        {
            Result<User> userResult = await _userRepository.GetUserByUidAsync(userUid: command.Request.UserUid);

            if (userResult.IsFailure)
            {
                return Result.Failed(error: userResult.Error, resultType: userResult.Type);
            }

            var user = userResult.Value!;

            user.MarkDelete();

            _userRepository.UpdateUser(user: user);

            await _userRepository.SaveChangesAsync();

            return Result.Success();
        }
    }
}
