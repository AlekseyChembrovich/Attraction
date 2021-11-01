using System.Linq;
using System.Collections.Generic;
using Attraction.DataAccessLayer.Models;
using Attraction.BusinessLayer.Interfaces;
using Attraction.DataAccessLayer.Repository;
using Attraction.BusinessLayer.Dto.TypeEvent;
using Attraction.DataAccessLayer.Repository.EntityFramework;

namespace Attraction.BusinessLayer.Services
{
    public class TypeEventService : ITypeEventService
    {
        private readonly IRepository<TypeEvent> _repositoryTypeEvent;

        public TypeEventService(DatabaseContextEntityFramework databaseContext)
        {
            _repositoryTypeEvent = new RepositoryEntityFrameworkTypeEvent(databaseContext);
        }

        public IEnumerable<TypeEventDto> GetAll()
        {
            var models = _repositoryTypeEvent.GetAll();
            var modelsDto = models.Select(x => new TypeEventDto(x));
            return modelsDto;
        }

        public void Create(TypeEventDto dto)
        {
            var model = new DataAccessLayer.Models.TypeEvent
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };

            _repositoryTypeEvent.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryTypeEvent.GetById(id);
            _repositoryTypeEvent.Delete(model);
        }

        public TypeEventDto GetById(int id)
        {
            var model = _repositoryTypeEvent.GetById(id);
            return new TypeEventDto(model);
        }

        public TypeEventDto GetByName(string name)
        {
            var model = (_repositoryTypeEvent as RepositoryEntityFrameworkTypeEvent)?.GetByName(name);
            return new TypeEventDto(model);
        }

        public void Edit(TypeEventDto dto)
        {
            var model = new DataAccessLayer.Models.TypeEvent
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };

            _repositoryTypeEvent.Update(model);
        }
    }
}
