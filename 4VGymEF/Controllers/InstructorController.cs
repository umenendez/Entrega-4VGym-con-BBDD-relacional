using _4VGymEF.Models;
using _4VGymEF.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("instructors")]
public class InstructorController : ControllerBase
{
    private readonly InstructorRepository _repository;

    public InstructorController(InstructorRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// GET /instructors
    /// Devuelve el listado de instructores.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<Instructor>> FindInstructors(
        [FromQuery(Name = "first_initial")] string? firstInitial)
    {
        try
        {
            IEnumerable<Instructor> instructors = _repository.GetByFirstInitial(firstInitial);

            return Ok(instructors);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponse(1, "An internal server error occurred"));
        }
    }

    /// <summary>
    /// GET /instructors/{id}
    /// Devuelve un instructor por su ID.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public ActionResult<Instructor> FindInstructorById([FromRoute] int id)
    {
        try
        {
            Instructor? instructor = _repository.GetById(id);

            if (instructor == null)
            {
                return BadRequest(new ErrorResponse(2, "No instructor found"));
            }

            return Ok(instructor);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponse(1, "An internal server error occurred"));
        }
    }

    /// <summary>
    /// POST /instructors
    /// Añade un nuevo instructor.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(InstructorCreateDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public ActionResult<InstructorCreateDto> CreateInstructor(
        [FromBody] InstructorCreateDto instructor)
    {
        List<ErrorResponse> miListaErrores = new List<ErrorResponse>();

        try
        {
            if (instructor == null)
            {
                return BadRequest(new ErrorResponse(3, "Invalid instructor data"));
            }

            if (string.IsNullOrWhiteSpace(instructor.Name))
            {
                miListaErrores.Add(new ErrorResponse(4, "The name of the instructor is mandatory"));
            }

            if (string.IsNullOrWhiteSpace(instructor.Email))
            {
                miListaErrores.Add(new ErrorResponse(5, "The email of the instructor is mandatory"));
            }

            if (!Instructor.CheckEmail(instructor.Email))
            {
                miListaErrores.Add(new ErrorResponse(6, "The email of the instructor is not valid"));
            }

            if (string.IsNullOrWhiteSpace(instructor.Phone))
            {
                miListaErrores.Add(new ErrorResponse(7, "The phone of the instructor is mandatory"));
            }

            if (!Instructor.CheckPhone(instructor.Phone))
            {
                miListaErrores.Add(new ErrorResponse(8, "The phone of the instructor is not valid"));
            }

            if (!Uri.TryCreate(instructor.PhotoUrl, UriKind.Absolute, out Uri? uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
            {
                miListaErrores.Add(new ErrorResponse(9, "The url image of the instructor is not valid"));
            }

            if (_repository.CheckIfExistsWithNameMail(instructor))
            {
                miListaErrores.Add(new ErrorResponse(10, "The instructor already exists"));
            }

            if (miListaErrores.Count > 0)
            {
                return BadRequest(miListaErrores);
            }

            InstructorCreateDto? createdInstructor =
                _repository.Add(instructor, out ErrorResponse? errorResponse);

            if (errorResponse != null)
            {
                return BadRequest(errorResponse);
            }

            return StatusCode(StatusCodes.Status201Created, createdInstructor);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ErrorResponse(1, "An internal server error occurred"));
        }
    }
}