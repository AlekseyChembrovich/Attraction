using System.Collections.Generic;
using Attraction.BusinessLayer.Dto.Locality;

namespace Attraction.BusinessLayer.Interfaces
{
    public interface ILocalityService
    {
        void Create(LocalityDto dto);

        void Delete(int id);

        IEnumerable<LocalityDto> GetAll();

        LocalityDto GetById(int id);

        void Edit(LocalityDto dto);

        LocalityDto GetByName(string name);
    }
}
