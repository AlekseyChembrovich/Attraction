using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Attraction.DataAccessLayer.Models
{
    public class TypeEvent
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public TypeEvent()
        {
            Events = new HashSet<Event>();
        }
    }
}
