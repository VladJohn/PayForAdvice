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


                    var role = repo.Find(1);
                    role.Name = "administrator";
                    repo.Update(role);
                    uw.Save();
                    Console.Write(repo.Find(1).Name);
                }
            }
        
    }
}
