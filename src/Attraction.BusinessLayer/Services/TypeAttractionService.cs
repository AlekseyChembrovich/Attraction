using System.Linq;
using System.Collections.Generic;
using Attraction.DataAccessLayer.Models;
using Attraction.BusinessLayer.Interfaces;
using Attraction.DataAccessLayer.Repository;
using Attraction.BusinessLayer.Dto.TypeAttraction;
using Attraction.DataAccessLayer.Repository.EntityFramework;

namespace Attraction.BusinessLayer.Services
{
    public class TypeAttractionService : ITypeAttractionService
    {
        private readonly IRepository<TypeAttraction> _repositoryTypeAttraction;

        public TypeAttractionService(DatabaseContextEntityFramework databaseContext)
        {
            _repositoryTypeAttraction = new RepositoryEntityFrameworkTypeAttraction(databaseContext);
        }

        public IEnumerable<TypeAttractionDto> GetAll()
        {
            var models = _repositoryTypeAttraction.GetAll();
            var modelsDto = models.Select(x => new TypeAttractionDto(x));
            return modelsDto;
        }

        public void Create(TypeAttractionDto dto)
        {
            var model = new DataAccessLayer.Models.TypeAttraction
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };

            _repositoryTypeAttraction.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryTypeAttraction.GetById(id);
            _repositoryTypeAttraction.Delete(model);
        }

        public TypeAttractionDto GetById(int id)
        {
            var model = _repositoryTypeAttraction.GetById(id);
            return new TypeAttractionDto(model);
        }

        public TypeAttractionDto GetByName(string name)
        {
            var model = (_repositoryTypeAttraction as RepositoryEntityFrameworkTypeAttraction)?.GetByName(name);
            return new TypeAttractionDto(model);
        }

        public void Edit(TypeAttractionDto dto)
        {
            var model = new DataAccessLayer.Models.TypeAttraction
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };

            _repositoryTypeAttraction.Update(model);
        }
    }
}
