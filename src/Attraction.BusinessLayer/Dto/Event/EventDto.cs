using System;
using Attraction.BusinessLayer.Dto.TypeEvent;
using Attraction.BusinessLayer.Dto.Attraction;

namespace Attraction.BusinessLayer.Dto.Event
{
    public sealed class EventDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public string Description { get; set; }

        public int TypeEventId { get; set; }

        public int AttractionId { get; set; }

        public TypeEventDto TypeEventDto { get; set; }

        public AttractionDto AttractionDto { get; set; }

        public EventDto()
        {
            
        }

        public EventDto(DataAccessLayer.Models.Event eEvent)
        {
            Id = eEvent.Id;
            Name = eEvent.Name;
            Date = eEvent.Date;
            StartTime = eEvent.StartTime;
            Description = eEvent.Description;
            TypeEventId = eEvent.TypeEventId;
            AttractionId = eEvent.AttractionId;

            if (eEvent.TypeEvent != null)
            {
                TypeEventDto = new TypeEventDto(eEvent.TypeEvent);
            }

            if (eEvent.Attraction != null)
            {
                AttractionDto = new AttractionDto(eEvent.Attraction);
            }
        }
    }
}
