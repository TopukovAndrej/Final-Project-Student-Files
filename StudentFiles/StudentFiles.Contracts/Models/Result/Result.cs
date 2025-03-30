namespace StudentFiles.Contracts.Models.Result
{
    using StudentFiles.Contracts.Common;

    public class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error? Error { get; }

        public ResultType Type { get; }

        protected Result(bool isSuccess, Error? error, ResultType type)
        {
            IsSuccess = isSuccess;
            Error = error;
            Type = type;
        }

        public static Result Success() => new(isSuccess: true, error: null, type: ResultType.Success);

        public static Result Failed(Error? error, ResultType resultType) => new(isSuccess: false, error: error, type: resultType);
    }

    public class Result<T> : Result
    {
        public T? Value { get; }

        private Result(T value) : base(isSuccess: true, error: null, type: ResultType.Success) => Value = value;

        private Result(Error? error, ResultType resultType) : base(isSuccess: false, error: error, type: resultType) => Value = default;

        public static Result<T> Success(T value) => new(value: value);

        public static new Result<T> Failed(Error? error, ResultType resultType) => new(error: error, resultType: resultType);
    }
}
