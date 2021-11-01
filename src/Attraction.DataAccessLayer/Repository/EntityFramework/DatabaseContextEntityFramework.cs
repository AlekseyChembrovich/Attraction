using Microsoft.EntityFrameworkCore;
using Attraction.DataAccessLayer.Models;

namespace Attraction.DataAccessLayer.Repository.EntityFramework
{
    public class DatabaseContextEntityFramework : DbContext
    {
        public virtual DbSet<Event> Event { get; set; }

        public virtual DbSet<Models.Attraction> Attraction { get; set; }

        public virtual DbSet<Locality> Locality { get; set; }

        public virtual DbSet<TypeAttraction> TypeAttraction { get; set; }

        public virtual DbSet<TypeEvent> TypeEvent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-ALEKSEY\\SQLEXPRESS;Initial Catalog=Attraction;Integrated Security=True");
            }
        }

        public DatabaseContextEntityFramework(DbContextOptions options) : base(options)
        {

        }

        public DatabaseContextEntityFramework() { }
    }
}
