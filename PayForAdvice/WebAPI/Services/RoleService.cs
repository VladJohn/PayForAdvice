using Domain;
using Repository;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class RoleService
    {
        //add a new role to an user
        public RoleModel AddNewRole(RoleModel newRole)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repository = unitOfWork.GetRepository<Role>();
                repository.Add(new Role { Id = newRole.Id, Name = newRole.Name });
                unitOfWork.Save();
            }
            return newRole;
        }

        //based on an userId it will return his role(basic, advicer or admin)
        public RoleModel GetRole(int idUser)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var repository = unitOfWork.GetRepository<Role>();
                var foundRole = repository.GetAll().ToList().Where(x => x.Id.Equals(idUser)).FirstOrDefault();
                return (foundRole == null ? null : new RoleModel { Id = foundRole.Id, Name = foundRole.Name });
            }
        }
    }
}