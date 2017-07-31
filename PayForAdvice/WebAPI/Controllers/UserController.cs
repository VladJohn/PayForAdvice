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
          //  var token = HttpContext.Current.Request.Headers["TokenText"];
           // var service2 = new TokenService();
          //  var authorizedToken = service2.IsAuthorized(token);
         //   if (authorizedToken != null)
            {
                var service = new UserService();
                var users = service.GetAdvicersByCategory(idCategory);
                if (users == null)
                {
            //        service2.Update(authorizedToken.Id);
                    return NotFound();
                }
             //   service2.Update(authorizedToken.Id);
                return Ok(users);
            }
          //  return BadRequest();
        }

        public IHttpActionResult GetLogIn(string username, string password)
        {
            var service = new UserService();
            var found = service.LogIn(username, password);
            if (found == null)
            {
                return BadRequest();
            }
            return Ok(found);
        }

        public IHttpActionResult PostUser(UserModelForSignUp user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
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
            var token = HttpContext.Current.Request.Headers["TokenText"];
            var service2 = new TokenService();
            var authorizedToken = service2.IsAuthorized(token);
            if (authorizedToken != null)
            {
                if (!ModelState.IsValid)
                {
                    service2.Update(authorizedToken.Id);
                    return BadRequest();
                }
                var service = new UserService();
                var user = service.DeleteUser(idUser);
                if (user == null)
                {
                    service2.Update(authorizedToken.Id);
                    return NotFound();
                }
                service2.Update(authorizedToken.Id);
                return Ok(user);
            }
            return BadRequest();
        }

        public IHttpActionResult PutUserProfile([FromBody] UserModelForProfile user)
        {
            var token = HttpContext.Current.Request.Headers["TokenText"];
            var service2 = new TokenService();
            var authorizedToken = service2.IsAuthorized(token);
            if (authorizedToken != null)
            {
                if (!ModelState.IsValid)
                {
                    service2.Update(authorizedToken.Id);
                    return BadRequest();
                }
                var service = new UserService();
                var updated = service.UpdateUserProfile(user);
                if (updated == null)
                {
                    service2.Update(authorizedToken.Id);
                    return NotFound();
                }
                service2.Update(authorizedToken.Id);
                return Ok(updated);
            }
            return BadRequest();
        }

        public IHttpActionResult GetUser(int id)
        {
          /*  var token = HttpContext.Current.Request.Headers["TokenText"];
            var service2 = new TokenService();
            var authorizedToken = service2.IsAuthorized(token);
            if (authorizedToken != null)
            {*/
                if (!ModelState.IsValid)
                {
                   // service2.Update(authorizedToken.Id);
                    return BadRequest();
                }
                var service = new UserService();
                var user = service.GetUser(id);
                if (user == null)
                {
                   // service2.Update(authorizedToken.Id);
                    return NotFound();
                }
               // service2.Update(authorizedToken.Id);
                return Ok(user);
           // }
          //  return BadRequest();
        }
    }
}