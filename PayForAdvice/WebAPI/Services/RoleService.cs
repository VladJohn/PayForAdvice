using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Mappings;
using WebAPI.Models;

namespace WebAPI.Service
{
    public class RoleService
    {
        public RoleModel Add(RoleModel newRole)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<Role>();
                repo.Add(RoleMapper.MapRoleDataModel(newRole));
                uw.Save();
            }
            return newRole;
        }

        public RoleModel Get(int idUser)
        {
            using (var uw = new UnitOfWork())
            {
                var repo = uw.GetRepository<Role>();
                var found = repo.GetAll().ToList().Where(x => x.Id.Equals(idUser)).FirstOrDefault();
                if (found == null)
                    return null;
                return RoleMapper.MapRole(found);
            }
        }
    }
}