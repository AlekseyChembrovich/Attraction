using System;
using Attraction.BusinessLayer.Dto.Locality;
using Attraction.BusinessLayer.Dto.TypeAttraction;

namespace Attraction.BusinessLayer.Dto.Attraction
{
    public sealed class AttractionDto
    {
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

        public LocalityDto LocalityDto { get; set; }

        public TypeAttractionDto TypeAttractionDto { get; set; }

        public AttractionDto()
        {
            
        }

        public AttractionDto(DataAccessLayer.Models.Attraction attraction)
        {
            Id = attraction.Id;
            Name = attraction.Name;
            FoundationDate = attraction.FoundationDate;
            Description = attraction.Description;
            Image = attraction.Image;
            Phone = attraction.Phone;
            IsRoundСlock = attraction.IsRoundСlock;
            StartTime = attraction.StartTime;
            EndTime = attraction.EndTime;
            LocalityId = attraction.LocalityId;
            TypeAttractionId = attraction.TypeAttractionId;
            if (attraction.Locality != null)
            {
                LocalityDto = new LocalityDto(attraction.Locality);
            }

            if (attraction.TypeAttraction != null)
            {
                TypeAttractionDto = new TypeAttractionDto(attraction.TypeAttraction);
            }
        }
    }
}
