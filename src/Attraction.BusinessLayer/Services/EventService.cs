using System.Linq;
using System.Collections.Generic;
using Attraction.BusinessLayer.Dto.Event;
using Attraction.BusinessLayer.Interfaces;
using Attraction.DataAccessLayer.Repository.EntityFramework;
using Attraction.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Attraction.BusinessLayer.Services
{
    public class EventService : IEventService
    {
        private readonly IRepositoryEvent _repositoryEvent;

        public EventService(DatabaseContextEntityFramework databaseContext)
        {
            _repositoryEvent = new RepositoryEntityFrameworkEvent(databaseContext);
        }

        public IEnumerable<EventDto> GetAll()
        {
            var models = _repositoryEvent.GetAllIncludeForeignKey();
            var modelsDto = models.Select(x => new EventDto(x));
            return modelsDto;
        }

        public void Create(EventDto dto)
        {
            var model = new DataAccessLayer.Models.Event
            {
                Id = dto.Id,
                Name = dto.Name,
                Date = dto.Date,
                StartTime = dto.StartTime,
                Description = dto.Description,
                TypeEventId = dto.TypeEventId,
                AttractionId = dto.AttractionId,
            };

            _repositoryEvent.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryEvent.GetById(id);
            _repositoryEvent.Delete(model);
        }

        public EventDto GetById(int id)
        {
            var model = _repositoryEvent.GetById(id);
            return new EventDto(model);
        }

        public void Edit(EventDto dto)
        {
            var model = new DataAccessLayer.Models.Event
            {
                Id = dto.Id,
                Name = dto.Name,
                Date = dto.Date,
                StartTime = dto.StartTime,
                Description = dto.Description,
                TypeEventId = dto.TypeEventId,
                AttractionId = dto.AttractionId,
            };

            _repositoryEvent.Update(model);
        }
    }
}
