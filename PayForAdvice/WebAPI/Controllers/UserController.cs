using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI.FacebookIntegration.Models;
using WebAPI.FacebookIntegration.Service;
using WebAPI.Models;
using WebAPI.Services;
using System;
using WebAPI.ValidatorsModel;
using System.Net.Http;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        private const string appId = "128919027720116";
        private const string redirectUri = "http://localhost:52619/api/user/";
        private const string appSecret = "cc0e61fc3c34b4ebbc2bf573290aeb9c";

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

        [System.Web.Http.HttpGet]
        public IHttpActionResult DeleteToken(string tokenText)
        {
            var token = HttpContext.Current.Request.Headers["TokenText"];
            var tokenService = new TokenService();
            var authorizedToken = tokenService.IsAuthorizedBase(token);
            if (authorizedToken == null)
            {
                authorizedToken = tokenService.IsAuthorizedAdviser(token);
                if(authorizedToken == null)
                {
                    authorizedToken = tokenService.IsAuthorizedAdmin(token);
                }
            }
            if (authorizedToken != null)
            {
                var deletedToken = tokenService.UnAuthorize(token);
                if (deletedToken == null)
                {
                    return NotFound();
                }
                return Ok(deletedToken);
            }
            return BadRequest();
        }

        public IHttpActionResult GetLogIn(string username, string password)
        {
            var userService = new UserService();
            var token = userService.LogIn(username, password);
            if (token == null)
            {
                return BadRequest("Wrong username or password.");
            }
            return Ok(token);
        }

        public IHttpActionResult PostUser(UserModelForSignUp user)
        {
            var userService = new UserService();
            UserModelForSignUp userAdded = null;
            try
            {
                userAdded = userService.AddUser(user);
            }
            catch (ModelException exception)
            {
                return BadRequest(exception.Message);
            }
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

        [System.Web.Mvc.HttpPost]
        public ActionResult LoginWithFacebook(string returnUrl)
        {
            var uri = $"https://www.facebook.com/v2.10/dialog/oauth?client_id={appId}&redirect_uri={redirectUri}&display=popup";
            return new RedirectResult(uri);
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public async Task<HttpResponseMessage> SignInCallBackAsync(string code)
        {
            //getting the access_token from the code
            var uri = $"https://graph.facebook.com/v2.10/oauth/access_token?client_id={appId}&redirect_uri={redirectUri}&client_secret={appSecret}&code={code}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            FbTokenModel res = JsonConvert.DeserializeObject<FbTokenModel>(StreamReader(resStream));

            //getting user data by access_token
            UserFacebookModel a = await Get(res.access_token);

            //check if user has logged in with fb before. If he did, log him in, else create him an account and log him in
            var service = new UserService();
            var service2 = new TokenService();
            if(!service.FindUserByEmail(a))
            {
                service.AddUserFromFacebook(a.Name, a.Email, a.picture.data.url);
            }
            var token = service.LogInWithFacebook(res.access_token, a.Email);
            var response3 = Request.CreateResponse(HttpStatusCode.Found);
            response3.Headers.Location = new Uri($"http://localhost:8080");
            var response4 = Request.CreateResponse(HttpStatusCode.Found);
            response4.Headers.Location = new Uri($"http://localhost:8080/main/{token.TokenText}");
            if (token == null)
            {
                return response3;
            }
            return response4;
        }

        [System.Web.Http.HttpGet]
        public async Task<UserFacebookModel> Get(string AT)
        {
            FacebookService facebookService = new FacebookService(AT);
            return await facebookService.GetFbUserDetails();
        }

        private string StreamReader(Stream resStream)
        {
            string token = "";
            using (var stream = new MemoryStream())
            {
                byte[] buffer = new byte[2048];
                int bytesRead;
                while ((bytesRead = resStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                }
                byte[] result = stream.ToArray();
                foreach (byte x in result)
                {
                    token = token + ((char)x).ToString();
                }
            }
            return token;
        }

    }
}