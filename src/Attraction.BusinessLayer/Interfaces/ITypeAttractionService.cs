using System.Collections.Generic;
using Attraction.BusinessLayer.Dto.TypeAttraction;

namespace Attraction.BusinessLayer.Interfaces
{
    public interface ITypeAttractionService
    {
        void Create(TypeAttractionDto dto);

        void Delete(int id);

        IEnumerable<TypeAttractionDto> GetAll();

        TypeAttractionDto GetById(int id);

        void Edit(TypeAttractionDto dto);

        TypeAttractionDto GetByName(string name);
    }
}
