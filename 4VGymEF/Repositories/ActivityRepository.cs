using _4VGymEF.Data;
using _4VGymEF.Dtos;
using _4VGymEF.Models;

namespace _4VGymEF.Repositories
{
    public class ActivityRepository
    {
        private readonly GymDbContext _bdGymContext;

        public ActivityRepository(GymDbContext gymDbContext)
        {
            _bdGymContext = gymDbContext;
        }

        public List<ActivityGetDto> GetAll()
        {
            return _bdGymContext.Activities.Select(a => new ActivityGetDto
            {
                Id = a.Id,
                ActivityTypeId = a.ActivityTypeId,
                InstructorsId = a.ListaInstructores.Select(i => i.Id).ToList(),
                MaxParticipants = a.MaxParticipants,
                DateStart = a.DateStart,
                DateEnd = a.DateEnd

            }).ToList();
        }

        public ActivityCreateDto? Add(ActivityCreateDto activity, out List<ErrorResponse> errors)
        {
            errors = new List<ErrorResponse>();

            ActivityType? activityType = _bdGymContext.ActivityTypes
                .FirstOrDefault(at => at.Id == activity.ActivityTypeId);

            if (activityType == null)
            {
                errors.Add(new ErrorResponse(31, "El tipo de actividad no existe."));
            }
            else if (activity.InstructorsId == null ||
                     activity.InstructorsId.Distinct().Count() < activityType.NumberInstructors)
            {
                errors.Add(new ErrorResponse(32, "La actividad no tiene suficientes monitores."));
            }

            if (activity.InstructorsId != null)
            {
                foreach (int id in activity.InstructorsId)
                {
                    if (!_bdGymContext.Instructors.Any(i => i.Id == id))
                    {
                        errors.Add(new ErrorResponse(35,
                            $"El monitor con ID {id} no existe."));
                    }
                }

                bool monitorBusy = _bdGymContext.Activities.Any(a =>
                    activity.DateStart < a.DateEnd &&
                    activity.DateEnd > a.DateStart &&
                    a.ListaInstructores.Any(i => activity.InstructorsId.Contains(i.Id)));

                if (monitorBusy)
                {
                    errors.Add(new ErrorResponse(34,
                        "Uno o varios monitores ya están asignados a otra actividad en ese horario."));
                }

                if (activity.InstructorsId.Count != activity.InstructorsId.Distinct().Count())
                {
                    errors.Add(new ErrorResponse(38,
                        "No se puede asignar el mismo monitor varias veces."));
                }
            }

            if (errors.Count > 0)
            {
                return null;
            }

            Activity nuevaActividad = new Activity
            {
                ActivityTypeId = activity.ActivityTypeId,
                MaxParticipants = activity.MaxParticipants,
                DateStart = activity.DateStart,
                DateEnd = activity.DateEnd,
                ListaInstructores = _bdGymContext.Instructors
                    .Where(i => activity.InstructorsId!.Contains(i.Id))
                    .ToList()
            };

            _bdGymContext.Activities.Add(nuevaActividad);
            _bdGymContext.SaveChanges();

            return activity;
        }
    }
}