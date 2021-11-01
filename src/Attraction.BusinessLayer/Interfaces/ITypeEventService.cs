using System.Collections.Generic;
using Attraction.BusinessLayer.Dto.TypeEvent;

namespace Attraction.BusinessLayer.Interfaces
{
    public interface ITypeEventService
    {
        void Create(TypeEventDto dto);

        void Delete(int id);

        IEnumerable<TypeEventDto> GetAll();

        TypeEventDto GetById(int id);

        void Edit(TypeEventDto dto);

        TypeEventDto GetByName(string name);
    }
}
