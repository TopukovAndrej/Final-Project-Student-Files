namespace StudentFiles.Contracts.Models.Result
{
    public class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public string? ErrorMessage { get; }

        public ResultType Type { get; }

        protected Result(bool isSuccess, string? errorMessage, ResultType type)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Type = type;
        }

        public static Result Success() => new(isSuccess: true, errorMessage: null, type: ResultType.Success);

        public static Result Failed(string? errorMessage, ResultType resultType) => new(isSuccess: false,
                                                                                        errorMessage: errorMessage,
                                                                                        type: resultType);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(T value) : base(isSuccess: true, errorMessage: null, type: ResultType.Success) => Value = value;

        private Result(string? errorMessage, ResultType resultType) : base(isSuccess: false,
                                                                           errorMessage: errorMessage,
                                                                           type: resultType) => Value = default;

        public static Result<T> Success(T value) => new(value: value);

        public static new Result<T> Failed(string? errorMessage, ResultType resultType) => new(errorMessage: errorMessage,
                                                                                               resultType: resultType);
    }
}
