using _4VGymEF.Data;
using _4VGymEF.Models;

namespace _4VGymEF.Repositories
{
    public class InstructorRepository
    {
        private readonly GymDbContext _bdGymContext;

        public InstructorRepository(GymDbContext gymDbContext)
        {
            _bdGymContext = gymDbContext;
        }

        public List<Instructor> GetAll()
        {
            return _bdGymContext.Instructors.ToList();
        }

        public Instructor? GetById(int id)
        {
            return _bdGymContext.Instructors
                .FirstOrDefault(i => i.Id == id);
        }

        public List<Instructor> GetByFirstInitial(string? firstInitial = null)
        {
            if (string.IsNullOrWhiteSpace(firstInitial))
            {
                return GetAll();
            }

            return _bdGymContext.Instructors
                .Where(i => i.Name.StartsWith(firstInitial))
                .ToList();
        }

        public List<Instructor> GetByFirstInitial2(string? firstInitial = null)
        {
            var instructors = _bdGymContext.Instructors.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstInitial))
            {
                instructors = instructors
                    .Where(i => i.Name.StartsWith(firstInitial));
            }

            return instructors.ToList();
        }

        public InstructorCreateDto? Add(InstructorCreateDto instructor, out ErrorResponse? errorResponse)
        {
            errorResponse = null;

            if (instructor == null)
            {
                errorResponse = new ErrorResponse(3, "Invalid instructor data");
                return null;
            }

            if (string.IsNullOrWhiteSpace(instructor.Name))
            {
                errorResponse = new ErrorResponse(4, "The name of the instructor is mandatory");
                return null;
            }

            Instructor nuevoInstructor = new Instructor
            {
                Name = instructor.Name,
                Email = instructor.Email,
                PhotoUrl = instructor.PhotoUrl,
                Phone = instructor.Phone
            };

            _bdGymContext.Instructors.Add(nuevoInstructor);
            _bdGymContext.SaveChanges();

            instructor.Id = nuevoInstructor.Id;

            return instructor;
        }

        public bool CheckIfExistsWithNameMail(InstructorCreateDto instructor)
        {
            return _bdGymContext.Instructors.Any(i =>
                i.Name.ToLower() == instructor.Name.ToLower() &&
                i.Email.ToLower() == instructor.Email.ToLower());
        }
    }
}