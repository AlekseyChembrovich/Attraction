using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Attraction.DataAccessLayer.Models
{
    public class Locality
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; }

        public Locality()
        {
            Attractions = new HashSet<Attraction>();
        }
    }
}
