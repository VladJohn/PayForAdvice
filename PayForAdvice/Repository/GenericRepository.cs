using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<E> : IRepository<E> where E : Idable
    {
        private DbSet<E> objs;
        private DatabaseContext dbc;

        public GenericRepository(DatabaseContext dbc)
        {
            this.objs = dbc.Set<E>();
            this.dbc = dbc;
        }

        public void Add(E entity)
        {
            objs.Add(entity);
        }

        public void Remove(int id)
        {
            E entity = Find(id);
            objs.Remove(entity);
        }

        public E Find(int id)
        {
            //return objs.Find(id);
            return objs.FirstOrDefault(s => s.Id == id);
        }

        public void Update(E entityToUpdate)
        {
            var entry = dbc.Entry(entityToUpdate);
            entry.State = EntityState.Modified;
        }

        public IQueryable<E> GetAll()
        {
            return objs;
        }


    }

}
