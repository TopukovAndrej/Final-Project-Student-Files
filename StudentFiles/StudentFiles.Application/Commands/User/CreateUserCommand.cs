namespace StudentFiles.Application.Commands.User
{
    using BCrypt.Net;
    using MediatR;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Contracts.Requests.User;
    using StudentFiles.Domain.Entities.User;
    using StudentFiles.Infrastructure.Data.Repositories.User;
    using System.Text;
    using System.Text.RegularExpressions;

    public class CreateUserCommand : IRequest<Result<Guid>>
    {
        public CreateUserRequest Request { get; set; }

        public CreateUserCommand(CreateUserRequest request)
        {
            Request = request;
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            if (!IsEmailValid(email: command.Request.Username))
            {
                return Result<Guid>.Failed(error: new Error(Code: ErrorCodes.UserEmailNotValid, Message: ErrorMessage.UserEmailNotValid), resultType: ResultType.BadRequest);
            }

            if (!IsPasswordValid(password: command.Request.Password))
            {
                return Result<Guid>.Failed(error: new Error(Code: ErrorCodes.UserPasswordNotValid, Message: ErrorMessage.UserPasswordNotValid), resultType: ResultType.BadRequest);
            }

            bool userAlreadyExists = await _userRepository.CheckIfUserExistsByUsernameAsync(username:  command.Request.Username);

            if (userAlreadyExists)
            {
                return Result<Guid>.Failed(error: new Error(Code: ErrorCodes.UserAlreadyExists, Message: ErrorMessage.UserAlreadyExists), resultType: ResultType.Conflict);
            }

            Result<User> newUserResult = User.Create(id: 0,
                                                     uid: Guid.NewGuid(),
                                                     isDeleted: false,
                                                     username: command.Request.Username,
                                                     hashedPassword: BCrypt.HashPassword(inputKey: Encoding.UTF8.GetString(bytes: Encoding.Default.GetBytes(s: command.Request.Password)),
                                                                                         salt: BCrypt.GenerateSalt()),
                                                     role: command.Request.Role);

            if (newUserResult.IsFailure)
            {
                return Result<Guid>.Failed(error: newUserResult.Error, resultType: newUserResult.Type);
            }

            await _userRepository.InsertUserAsync(user: newUserResult.Value!);

            await _userRepository.SaveChangesAsync();

            return Result<Guid>.Success(value: newUserResult.Value!.Uid);
        }

        private static bool IsEmailValid(string email)
        {
            if (email.Length == 0)
            {
                return false;
            }

            string[] emailParts = email.Split(separator: '@');

            if (emailParts.Length != 2)
            {
                return false;
            }

            return emailParts[1] == "students.edu";
        }

        private static bool IsPasswordValid(string password)
        {
            if (password.Length < 7)
            {
                return false;
            }

            int numberOfLetters = 0;
            int numberOfDigits = 0;
            int numberOfSpecialCharacters = 0;

            for (int i = 0; i < password.Length; i++)
            {
                string passwordCharacter = password[i].ToString();

                if (Regex.IsMatch(input: passwordCharacter, pattern: @"[a-zA-Z]"))
                {
                    numberOfLetters++;
                }

                if (password[i] >= '0' && password[i] <= '9')
                {
                    numberOfDigits++;
                }

                if (Regex.IsMatch(input: passwordCharacter, pattern: @"[`~@#$%^&*()_+}{"":?><}!]"))
                {
                    numberOfSpecialCharacters++;
                }
            }

            return numberOfLetters >= 3 && numberOfDigits >= 3 && numberOfSpecialCharacters >= 1;
        }
    }
}
