using System.Collections.Generic;

namespace Attraction.DataAccessLayer.Repository.EntityFramework.Interfaces
{
    public interface IRepositoryAttraction : IRepository<Models.Attraction>
    {
        IEnumerable<Models.Attraction> GetAllIncludeForeignKey();
    }
}
