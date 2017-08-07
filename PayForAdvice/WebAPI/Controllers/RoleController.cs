using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    public class RoleController : ApiController
    {
        public IHttpActionResult GetRoles(int IdRole)
        {
            var service = new RoleService();
            var roles = service.GetRole(IdRole);
            if (roles == null)
                return NotFound();
            return Ok(roles);
        }
    }
}