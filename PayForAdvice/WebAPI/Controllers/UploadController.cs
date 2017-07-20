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
        public IHttpActionResult Add([FromBody]UploadModel upl)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new UploadService();
            var ac = service.Register(upl);
            if (ac == null)
            {
                return BadRequest();
            }
            return Ok(ac);
        }
    }
}