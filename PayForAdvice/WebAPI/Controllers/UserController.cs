using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.FacebookIntegration.Models;
using WebAPI.FacebookIntegration.Service;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult GetUsersByCategory(int categoryId)
        {
            var token = HttpContext.Current.Request.Headers["TokenText"];
            var tokenService = new TokenService();
            var authorizedToken = tokenService.IsAuthorizedBase(token);
            if (authorizedToken != null)
            {
                var userService = new UserService();
                var userList = userService.GetAdvicersByCategory(categoryId);
                if (userList == null)
                {
                    tokenService.Update(authorizedToken.Id);
                    return NotFound();
                }
                tokenService.Update(authorizedToken.Id);
                return Ok(userList);
            }
            return BadRequest();
        }

        public IHttpActionResult GetLogIn(string username, string password)
        {
            var userService = new UserService();
            var token = userService.LogIn(username, password);
            if (token == null)
            {
                return BadRequest();
            }
            return Ok(token);
        }

        public IHttpActionResult PostUser(UserModelForSignUp user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var userService = new UserService();
            var userAdded = userService.AddUser(user);
            if (userAdded == null)
            {
                return NotFound();
            }
            return Ok(userAdded);
        }

        public IHttpActionResult PutDeleteStatusOnUser(int userId)
        {
            var token = HttpContext.Current.Request.Headers["TokenText"];
            var tokenService = new TokenService();
            var authorizedToken = tokenService.IsAuthorizedBase(token);
            if (authorizedToken != null)
            {
                if (!ModelState.IsValid)
                {
                    tokenService.Update(authorizedToken.Id);
                    return BadRequest();
                }
                var userService = new UserService();
                var deletedUser = userService.DeleteUser(userId);
                if (deletedUser == null)
                {
                    tokenService.Update(authorizedToken.Id);
                    return NotFound();
                }
                tokenService.Update(authorizedToken.Id);
                return Ok(deletedUser);
            }
            return BadRequest();
        }

        public IHttpActionResult PutUserProfile([FromBody] UserModelForProfile user)
        {
            var token = HttpContext.Current.Request.Headers["TokenText"];
            var tokenService = new TokenService();
            var authorizedToken = tokenService.IsAuthorizedBase(token);
            if (authorizedToken != null)
            {
                if (!ModelState.IsValid)
                {
                    tokenService.Update(authorizedToken.Id);
                    return BadRequest();
                }
                var userService = new UserService();
                var updatedUserProfile = userService.UpdateUserProfile(user);
                if (updatedUserProfile == null)
                {
                    tokenService.Update(authorizedToken.Id);
                    return NotFound();
                }
                tokenService.Update(authorizedToken.Id);
                return Ok(updatedUserProfile);
            }
            return BadRequest();
        }

        public IHttpActionResult GetUser(int userId)
        {
            var token = HttpContext.Current.Request.Headers["TokenText"];
            var tokenService = new TokenService();
            var authorizedToken = tokenService.IsAuthorizedBase(token);
            if (authorizedToken != null)
            {
                if (!ModelState.IsValid)
                {
                    tokenService.Update(authorizedToken.Id);
                    return BadRequest();
                }
                var userService = new UserService();
                var userFound = userService.GetUser(userId);
                if (userFound == null)
                {
                    tokenService.Update(authorizedToken.Id);
                    return NotFound();
                }
                tokenService.Update(authorizedToken.Id);
                return Ok(userFound);
            }
            return BadRequest();
        }


        [System.Web.Http.HttpGet]
        public async Task<UserFacebookModel> Get()
        {
            FacebookService facebookService = new FacebookService();
            return await facebookService.GetFbUserDetails();
        }

        public IHttpActionResult GetUserData(string userData)
        {
            var token = HttpContext.Current.Request.Headers["TokenText"];
            var tokenService = new TokenService();
            var authorizedToken = tokenService.IsAuthorizedBase(token);
            if (authorizedToken != null)
            {
                if (!ModelState.IsValid)
                {
                    tokenService.Update(authorizedToken.Id);
                    return BadRequest();
                }
                var userReturnedData = tokenService.getUserInfoByToken(token);
                if (userReturnedData == null)
                {
                    tokenService.Update(authorizedToken.Id);
                    return NotFound();
                }
                tokenService.Update(authorizedToken.Id);
                return Ok(userReturnedData);
            }
            return BadRequest();
        }

        [System.Web.Http.Route("api/user/{code}")]
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult SignInCallBack(string code)
        {
            //var callbackUrl = Url.Action("ConfirmEmail", "user", new { code = code }, Request.RequestUri.Scheme);
            return Ok(code);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult LoginWithFacebook(string returnUrl)
        {
            var appId = "128919027720116";
            var redirectUri = "http://localhost:8080/user/";
            var uri = $"https://www.facebook.com/v2.10/dialog/oauth?client_id={appId}&redirect_uri={redirectUri}";
            return new RedirectResult(uri);
        }
    }
}