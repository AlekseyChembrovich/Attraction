namespace Attraction.BusinessLayer.Dto.TypeEvent
{
    public class TypeEventDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TypeEventDto()
        {
            
        }

        public TypeEventDto(DataAccessLayer.Models.TypeEvent typeAttraction)
        {
            Id = typeAttraction.Id;
            Name = typeAttraction.Name;
            Description = typeAttraction.Description;
        }
    }
}
