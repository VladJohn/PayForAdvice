using System.Web.Http;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class RoleController : ApiController
    {
        //get the role with the id = idRole for a user
        public IHttpActionResult GetRoles(int idRole)
        {
            var service = new RoleService();
            var roles = service.GetRole(idRole);
            if (roles == null)
                return NotFound();
            return Ok(roles);
        }
    }
}