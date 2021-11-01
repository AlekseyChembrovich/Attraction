using System.Collections.Generic;
using Attraction.BusinessLayer.Dto.Attraction;

namespace Attraction.BusinessLayer.Interfaces
{
    public interface IAttractionService
    {
        void Create(AttractionDto dto);

        void Delete(int id);

        IEnumerable<AttractionDto> GetAll();

        AttractionDto GetById(int id);

        void Edit(AttractionDto dto);

        AttractionDto GetByName(string name);
    }
}
