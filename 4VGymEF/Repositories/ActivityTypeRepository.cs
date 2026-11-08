using _4VGymEF.Data;
using _4VGymEF.Models;

namespace _4VGymEF.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly GymDbContext _bdGymContext;

        public ActivityTypeRepository(GymDbContext gymDbContext)
        {
            _bdGymContext = gymDbContext;
        }

        public List<ActivityType> GetAll()
        {
            return _bdGymContext.ActivityTypes.ToList();
        }

        public ActivityType? GetByName(string name)
        {
            return _bdGymContext.ActivityTypes
                .FirstOrDefault(at => at.Name.ToLower() == name.ToLower());
        }

        public ActivityTypeCreateDto? Add(ActivityTypeCreateDto activityType, out ErrorResponse? errorResponse)
        {
            errorResponse = null;

            if (activityType == null)
            {
                errorResponse = new ErrorResponse(3, "Invalid activity type data");
                return null;
            }

            if (string.IsNullOrWhiteSpace(activityType.Name))
            {
                errorResponse = new ErrorResponse(4, "The name of the activity is mandatory");
                return null;
            }

            if (activityType.NumberMonitors < 1)
            {
                activityType.NumberMonitors = 1;
            }

            if (GetByName(activityType.Name) != null)
            {
                errorResponse = new ErrorResponse(6,
                    "The name is unique, there is already another ActivityType with the same Name");
                return null;
            }

            ActivityType nuevoActivityType = new ActivityType
            {
                Name = activityType.Name,
                NumberInstructors = activityType.NumberMonitors
            };

            _bdGymContext.ActivityTypes.Add(nuevoActivityType);
            _bdGymContext.SaveChanges();

            activityType.Id = nuevoActivityType.Id;

            return activityType;
        }

        public ActivityType? GetById(long id)
        {
            return _bdGymContext.ActivityTypes
                .FirstOrDefault(at => at.Id == id);
        }
    }
}