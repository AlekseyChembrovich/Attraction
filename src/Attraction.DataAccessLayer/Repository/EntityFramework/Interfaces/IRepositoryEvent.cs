using System.Collections.Generic;
using Attraction.DataAccessLayer.Models;

namespace Attraction.DataAccessLayer.Repository.EntityFramework.Interfaces
{
    public interface IRepositoryEvent : IRepository<Event>
    {
        IEnumerable<Event> GetAllIncludeForeignKey();
    }
}
