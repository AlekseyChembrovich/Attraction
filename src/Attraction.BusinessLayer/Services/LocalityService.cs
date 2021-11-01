using System.Linq;
using System.Collections.Generic;
using Attraction.DataAccessLayer.Models;
using Attraction.BusinessLayer.Interfaces;
using Attraction.BusinessLayer.Dto.Locality;
using Attraction.DataAccessLayer.Repository;
using Attraction.DataAccessLayer.Repository.EntityFramework;

namespace Attraction.BusinessLayer.Services
{
    public class LocalityService : ILocalityService
    {
        private readonly IRepository<Locality> _repositoryLocality;

        public LocalityService(DatabaseContextEntityFramework databaseContext)
        {
            _repositoryLocality = new RepositoryEntityFrameworkLocality(databaseContext);
        }

        public IEnumerable<LocalityDto> GetAll()
        {
            var models = _repositoryLocality.GetAll();
            var modelsDto = models.Select(x => new LocalityDto(x));
            return modelsDto;
        }

        public void Create(LocalityDto dto)
        {
            var model = new DataAccessLayer.Models.Locality
            {
                Id = dto.Id,
                Name = dto.Name,
                Region = dto.Region,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };

            _repositoryLocality.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryLocality.GetById(id);
            _repositoryLocality.Delete(model);
        }

        public LocalityDto GetById(int id)
        {
            var model = _repositoryLocality.GetById(id);
            return new LocalityDto(model);
        }

        public LocalityDto GetByName(string name)
        {
            var model = (_repositoryLocality as RepositoryEntityFrameworkLocality)?.GetByName(name);
            return new LocalityDto(model);
        }

        public void Edit(LocalityDto dto)
        {
            var model = new DataAccessLayer.Models.Locality
            {
                Id = dto.Id,
                Name = dto.Name,
                Region = dto.Region,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude
            };

            _repositoryLocality.Update(model);
        }
    }
}
