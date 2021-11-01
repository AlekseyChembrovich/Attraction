namespace Attraction.BusinessLayer.Dto.TypeAttraction
{
    public class TypeAttractionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TypeAttractionDto()
        {
            
        }

        public TypeAttractionDto(DataAccessLayer.Models.TypeAttraction typeAttraction)
        {
            Id = typeAttraction.Id;
            Name = typeAttraction.Name;
            Description = typeAttraction.Description;
        }
    }
}
