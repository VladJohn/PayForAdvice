
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    //test comment
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

        //fix it
        public IHttpActionResult GetLogIn(string username, string password)
        {
            var service = new UserService();
            var found = service.LogIn(username, password);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        public IHttpActionResult PostUser([FromBody]UserModelForSignUp user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new UserService();
            var userAdded = service.AddUser(user);
            if (userAdded == null)
            {
                return NotFound();
            }
            return Ok(userAdded);
        }

        public IHttpActionResult PutUserDeleteStatus(int idUser)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new UserService();
            var user = service.DeleteUser(idUser);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        public IHttpActionResult PutUserProfile([FromBody] UserModelForProfile user)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new UserService();
            var updated = service.UpdateUserProfile(user);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }

        public IHttpActionResult GetUser(int id)
        {
            var service = new UserService();
            var user = service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


    }
}