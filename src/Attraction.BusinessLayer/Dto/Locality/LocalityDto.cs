namespace Attraction.BusinessLayer.Dto.Locality
{
    public class LocalityDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public LocalityDto()
        {
            
        }

        public LocalityDto(DataAccessLayer.Models.Locality locality)
        {
            Id = locality.Id;
            Name = locality.Name;
            Region = locality.Region;
            Address = locality.Address;
            Latitude = locality.Latitude;
            Longitude = locality.Longitude;
        }
    }
}
