using Microsoft.EntityFrameworkCore;
// Importa la carpeta donde tienes definidos tus modelos
using _4VGymEF.Models;


namespace _4VGymEF.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options)
            : base(options)
        {
        }
        // Indicamos las clases de modelo para mapearlas a tablas de SQL Server
        public DbSet<Activity> Activities { get; set; }
        // Añade el resto (piensa cuales debes poner para que se conviertan en tablas)
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
    }

}
