using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Attraction.DataAccessLayer.Models;
using Attraction.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Attraction.DataAccessLayer.Repository.EntityFramework
{
    public class RepositoryEntityFrameworkEvent : BaseRepositoryEntityFramework<Event>, IRepositoryEvent
    {
        public RepositoryEntityFrameworkEvent(DatabaseContextEntityFramework context) : base(context)
        {
        }

        public IEnumerable<Event> GetAllIncludeForeignKey()
        {
            return Context.Event
                .Include(x => x.Attraction)
                .Include(x => x.TypeEvent);
        }
    }
}
