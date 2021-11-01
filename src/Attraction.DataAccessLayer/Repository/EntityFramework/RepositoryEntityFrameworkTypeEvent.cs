using System.Linq;
using Attraction.DataAccessLayer.Models;

namespace Attraction.DataAccessLayer.Repository.EntityFramework
{
    public class RepositoryEntityFrameworkTypeEvent : BaseRepositoryEntityFramework<TypeEvent>
    {
        public RepositoryEntityFrameworkTypeEvent(DatabaseContextEntityFramework context) : base(context)
        {
        }

        public TypeEvent GetByName(string name)
        {
            return Context.TypeEvent.FirstOrDefault(x => x.Name.ToLower().Contains(name.ToLower()));
        }
    }
}
