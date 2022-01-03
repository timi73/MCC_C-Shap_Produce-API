using API.Context;
using API.Repository.Data;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key> where Entity : class where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;
        //private readonly UniversityRepository universityRepository;
        //var entityRepository = Entity

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            return entities.Find(key);
        }

        public int Insert(Entity entyti)
        {
            if (entyti == null){
                throw new ArgumentNullException("Entity");
            }
            //universityRepository.Check(entyti);
            entities.Add(entyti);
            return myContext.SaveChanges();
        }

        public int Update(Entity entyti)
        {
            if (entyti == null)
            {
                throw new ArgumentNullException("Entity");
            }
            myContext.Entry(entyti).State = EntityState.Modified;
            return myContext.SaveChanges();
        }

        public int Delete(Key key)
        {
            var entity = entities.Find(key);
            if (entity == null) {
                throw new ArgumentNullException("Entity");
            }
            entities.Remove(entity);
            return myContext.SaveChanges();
        }
    }
}
