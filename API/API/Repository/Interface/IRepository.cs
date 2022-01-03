using System.Collections.Generic;

namespace API.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {
        IEnumerable<Entity> Get();
        Entity Get(Key key);
        int Insert(Entity entyti);
        int Update(Entity entyti);
        int Delete(Key key);
    }
}
