using System.Web.Http;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class UploadController : ApiController
    {
        //POST
        public IHttpActionResult Post(UploadModel upload)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var uploadService = new UploadService();
            var uploadToAdd = uploadService.Register(upload);
            if (uploadToAdd == null)
            {
                return BadRequest();
            }
            return Ok(uploadToAdd);
        }
    }
}