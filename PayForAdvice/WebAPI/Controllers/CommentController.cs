using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class CommentController : ApiController
    {
        //GET
        public IHttpActionResult GetAllCommentsForQuestionId(int questionId)
        {
            var service = new CommentService();
            var comments = service.GetAllCommentsForQuestionId(questionId);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        //POST
        public IHttpActionResult Add([FromBody]CommentModel com)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new CommentService();
            var ac = service.Add(com);
            if (ac == null)
            {
                return BadRequest();
            }
            return Ok(ac);
        }
    }
}