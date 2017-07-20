using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class Program
    {
            static void Main(string[] args)
            {
                using (var uw = new UnitOfWork())
                {
                    var repo = uw.GetRepository<Role>();
                    var repoU = uw.GetRepository<User>();
                    foreach (var x in repoU.GetAll().ToList())
                    {
                        Console.Write(x.Name);
                    }
                }
            }
        
    }
}
