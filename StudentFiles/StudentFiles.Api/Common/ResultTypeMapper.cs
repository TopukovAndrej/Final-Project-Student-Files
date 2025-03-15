namespace StudentFiles.Api.Common
{
    using StudentFiles.Contracts.Models.Result;

    public static class ResultTypeMapper
    {
        public static int MapToHttpStatusCode(ResultType resultType)
        {
            return resultType switch
            {
                ResultType.Success => StatusCodes.Status200OK,
                ResultType.NotFound => StatusCodes.Status404NotFound,
                ResultType.Unauthorized => StatusCodes.Status401Unauthorized,
                ResultType.Invalid => StatusCodes.Status422UnprocessableEntity,
                ResultType.BadRequest => StatusCodes.Status400BadRequest,
                ResultType.Forbidden => StatusCodes.Status403Forbidden,
                ResultType.Conflict => StatusCodes.Status409Conflict,
                ResultType.InternalError => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError,
            };
        }
    }
}
