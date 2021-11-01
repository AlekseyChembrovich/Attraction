using System;
using System.ComponentModel.DataAnnotations;

namespace Attraction.DataAccessLayer.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public string Description { get; set; }

        public int TypeEventId { get; set; }

        public int AttractionId { get; set; }

        public virtual TypeEvent TypeEvent { get; set; }

        public virtual Attraction Attraction { get; set; }
    }
}
