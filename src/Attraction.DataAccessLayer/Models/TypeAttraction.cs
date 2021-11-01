using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Attraction.DataAccessLayer.Models
{
    public class TypeAttraction
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Attraction> Attractions { get; set; }

        public TypeAttraction()
        {
            Attractions = new HashSet<Attraction>();
        }
    }
}
