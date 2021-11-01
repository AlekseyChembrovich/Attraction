using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Attraction.DataAccessLayer.Repository.EntityFramework.Interfaces;

namespace Attraction.DataAccessLayer.Repository.EntityFramework
{
    public class RepositoryEntityFrameworkAttraction : BaseRepositoryEntityFramework<Models.Attraction>, IRepositoryAttraction
    {
        public RepositoryEntityFrameworkAttraction(DatabaseContextEntityFramework context) : base(context)
        {
        }

        public IEnumerable<Models.Attraction> GetAllIncludeForeignKey()
        {
            return Context.Attraction
                .Include(x => x.TypeAttraction)
                .Include(x => x.Locality);
        }

        public Models.Attraction GetByName(string name)
        {
            return Context.Attraction.FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
