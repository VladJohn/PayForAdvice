using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class UploadController : ApiController
    {
        //POST
        public IHttpActionResult Add(UploadModel upload)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new UploadService();
            var addUpload = service.Register(upload);
            if (addUpload == null)
            {
                return BadRequest();
            }
            return Ok(addUpload);
        }
    }
}