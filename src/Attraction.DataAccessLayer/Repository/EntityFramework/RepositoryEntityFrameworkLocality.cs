using System.Linq;
using Attraction.DataAccessLayer.Models;

namespace Attraction.DataAccessLayer.Repository.EntityFramework
{
    public class RepositoryEntityFrameworkLocality : BaseRepositoryEntityFramework<Locality>
    {
        public RepositoryEntityFrameworkLocality(DatabaseContextEntityFramework context) : base(context)
        {
        }

        public Locality GetByName(string name)
        {
            return Context.Locality.FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
