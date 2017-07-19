using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
        public class UnitOfWork : IDisposable
        {
            internal DatabaseContext context;
            internal Dictionary<string, object> repositories;

            public UnitOfWork()
            {
                context = new DatabaseContext();
                repositories = new Dictionary<string, object>();
            }

            public void Save()
            {
                context.SaveChanges();
            }

            public void Dispose()
            {
                context.Dispose();
                GC.SuppressFinalize(this);
            }

            public GenericRepository<E> GetRepository<E>() where E : Idable
            {
                var typeKey = typeof(E).Name;

                if (!repositories.ContainsKey(typeKey))
                {
                    var instance = new GenericRepository<E>(context);
                    repositories.Add(typeKey, instance);
                }
                return repositories[typeKey] as GenericRepository<E>;
            }

    
    }
}
