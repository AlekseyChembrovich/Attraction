using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Attraction.DataAccessLayer.Models
{
    public class Attraction
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime FoundationDate { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string Phone { get; set; }

        public bool IsRoundСlock { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int LocalityId { get; set; }

        public int TypeAttractionId { get; set; }

        public virtual Locality Locality { get; set; }

        public virtual TypeAttraction TypeAttraction { get; set; }

        public ICollection<Event> Events { get; set; }

        public Attraction()
        {
            Events = new HashSet<Event>();
        }
    }
}
