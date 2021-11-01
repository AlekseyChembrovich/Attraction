using System.Linq;
using Attraction.DataAccessLayer.Models;

namespace Attraction.DataAccessLayer.Repository.EntityFramework
{
    public class RepositoryEntityFrameworkTypeAttraction : BaseRepositoryEntityFramework<TypeAttraction>
    {
        public RepositoryEntityFrameworkTypeAttraction(DatabaseContextEntityFramework context) : base(context)
        {
        }

        public TypeAttraction GetByName(string name)
        {
            return Context.TypeAttraction.FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
