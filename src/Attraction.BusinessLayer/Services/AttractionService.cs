using System.Linq;
using System.Collections.Generic;
using Attraction.BusinessLayer.Interfaces;
using Attraction.BusinessLayer.Dto.Attraction;
using Attraction.DataAccessLayer.Repository.EntityFramework;
using Attraction.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Attraction.BusinessLayer.Services
{
    public class AttractionService : IAttractionService
    {
        private readonly IRepositoryAttraction _repositoryAttraction;

        public AttractionService(DatabaseContextEntityFramework databaseContext)
        {
            _repositoryAttraction = new RepositoryEntityFrameworkAttraction(databaseContext);
        }

        public IEnumerable<AttractionDto> GetAll()
        {
            var models = _repositoryAttraction.GetAllIncludeForeignKey();
            var modelsDto = models.Select(x => new AttractionDto(x));
            return modelsDto;
        }

        public void Create(AttractionDto dto)
        {
            var model = new DataAccessLayer.Models.Attraction
            {
                Id = dto.Id,
                Name = dto.Name,
                FoundationDate = dto.FoundationDate,
                Description = dto.Description,
                Image = dto.Image,
                Phone = dto.Phone,
                IsRoundСlock = dto.IsRoundСlock,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                LocalityId = dto.LocalityId,
                TypeAttractionId = dto.TypeAttractionId
            };

            _repositoryAttraction.Insert(model);
        }

        public void Delete(int id)
        {
            var model = _repositoryAttraction.GetById(id);
            _repositoryAttraction.Delete(model);
        }

        public AttractionDto GetById(int id)
        {
            var model = _repositoryAttraction.GetById(id);
            return new AttractionDto(model);
        }

        public AttractionDto GetByName(string name)
        {
            var model = (_repositoryAttraction as RepositoryEntityFrameworkAttraction)?.GetByName(name);
            return new AttractionDto(model);
        }

        public void Edit(AttractionDto dto)
        {
            var model = new DataAccessLayer.Models.Attraction
            {
                Id = dto.Id,
                Name = dto.Name,
                FoundationDate = dto.FoundationDate,
                Description = dto.Description,
                Image = dto.Image,
                Phone = dto.Phone,
                IsRoundСlock = dto.IsRoundСlock,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                LocalityId = dto.LocalityId,
                TypeAttractionId = dto.TypeAttractionId
            };

            _repositoryAttraction.Update(model);
        }
    }
}
