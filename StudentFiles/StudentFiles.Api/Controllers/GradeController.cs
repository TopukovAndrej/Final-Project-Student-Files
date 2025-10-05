namespace StudentFiles.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentFiles.Api.Common;
    using StudentFiles.Application.Commands.Grade;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Models.Result;
    using StudentFiles.Contracts.Requests.Grade;

    [ApiController]
    [Route("/api/grades")]
    public class GradeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GradeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Professor)]
        [Route("submit")]
        public async Task<IActionResult> SubmitGradeAsync([FromBody] SubmitGradeRequest request)
        {
            Result result = await _mediator.Send(request: new SubmitGradeCommand(request: request));

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }
    }
}
