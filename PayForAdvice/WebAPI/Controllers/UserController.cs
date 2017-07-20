using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult GetUsersByCategory(int idCategory)
        {
            var service = new UserService();
            var users = service.GetAdvicersByCategory(idCategory);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
    }
}