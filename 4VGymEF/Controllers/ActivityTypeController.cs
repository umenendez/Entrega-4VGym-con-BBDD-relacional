using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("activity-types")]
public class ActivityTypeController : ControllerBase
{
    private readonly ActivityTypeRepository _repository;

    public ActivityTypeController(ActivityTypeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActivityType>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<ActivityType>> FindActivityTypes()
    {
        try
        {
            IEnumerable<ActivityType> activityTypes = _repository.GetAll();

            if (!activityTypes.Any())
            {
                return BadRequest(new ErrorResponse(2, "No activity types found"));
            }

            return Ok(activityTypes);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponse(1, "An internal server error occurred"));
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ActivityTypeCreateDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<ActivityTypeCreateDto> CreateActivityType(
        [FromBody] ActivityTypeCreateDto activityType)
    {
        try
        {
            if (activityType == null)
            {
                return BadRequest(new ErrorResponse(3, "Invalid activity type data"));
            }

            if (string.IsNullOrWhiteSpace(activityType.Name))
            {
                return BadRequest(new ErrorResponse(4, "The name of the activity is mandatory"));
            }

            ErrorResponse? errorResponse;

            ActivityTypeCreateDto? createdActivityType =
                _repository.Add(activityType, out errorResponse);

            if (errorResponse != null)
            {
                return BadRequest(errorResponse);
            }

            return StatusCode(StatusCodes.Status201Created, createdActivityType);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponse(1, "An internal server error occurred"));
        }
    }
}