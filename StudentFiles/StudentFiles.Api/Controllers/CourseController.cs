namespace StudentFiles.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StudentFiles.Api.Common;
    using StudentFiles.Application.Queries.Course;
    using StudentFiles.Contracts.Common;
    using StudentFiles.Contracts.Dtos.Course;
    using StudentFiles.Contracts.Models.Result;

    [ApiController]
    [Route("/api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = UserRole.Professor)]
        [Route("professor/{professorUid:guid}")]
        public async Task<IActionResult> GetProfessorCoursesAsync([FromRoute] Guid professorUid)
        {
            Result<IReadOnlyList<CourseDto>> result = await _mediator.Send(request: new GetProfessorCoursesQuery(professorUid: professorUid));

            return StatusCode(statusCode: ResultTypeMapper.MapToHttpStatusCode(resultType: result.Type), value: result);
        }
    }
}
