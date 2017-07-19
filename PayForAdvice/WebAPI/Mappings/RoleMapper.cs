using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class RoleMapper
    {
        public static RoleModel MapRole(Role role)
        {
            return new RoleModel { Id = role.Id, Name = role.Name };
        }

        public static Role MapRoleDataModel (RoleModel role)
        {
            return new Role { Id = role.Id, Name = role.Name };
        }
    }
}