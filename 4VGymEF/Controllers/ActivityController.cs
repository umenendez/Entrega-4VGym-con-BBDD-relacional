using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGymEF.Controllers
{
    [ApiController]
    [Route("activities")]
    public class ActivityController : ControllerBase
    {
        private readonly ActivityRepository _repository;

        public ActivityController(ActivityRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// POST /activities
        /// Añade una nueva actividad en 4VGym.
        /// </summary>
        /// <param name="activity">Objeto Activity enviado en el cuerpo de la petición (JSON).</param>
        [HttpPost]
        [ProducesResponseType(typeof(ActivityCreateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult CreateActivity([FromBody] ActivityCreateDto activity)
        {
            try
            {
                ActivityCreateDto? activityCreated = _repository.Add(activity, out List<ErrorResponse> errors);

                if (activityCreated == null && errors.Count > 0)
                {
                    return BadRequest(errors);
                }
                return StatusCode(StatusCodes.Status201Created, activityCreated);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse(1, "An internal server error occurred"));
            }
        }
    }
}