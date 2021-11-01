using System.Collections.Generic;
using Attraction.BusinessLayer.Dto.Event;

namespace Attraction.BusinessLayer.Interfaces
{
    public interface IEventService
    {
        void Create(EventDto dto);

        void Delete(int id);

        IEnumerable<EventDto> GetAll();

        EventDto GetById(int id);

        void Edit(EventDto dto);
    }
}
