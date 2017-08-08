using System.Web.Http;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class CommentController : ApiController
    {
        //GET all comments for a question given by id = questionId
        public IHttpActionResult GetAllCommentsForQuestionId(int questionId)
        {
            var service = new CommentService();
            var comments = service.GetAllCommentsByQuestionId(questionId);
            if (comments == null)
            {
                return NotFound();
            }
            return Ok(comments);
        }

        //POST - add a new comment to a question
        public IHttpActionResult Add(CommentModel comment)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new CommentService();
            var addComent = service.AddComment(comment);
            if (addComent == null)
            {
                return BadRequest();
            }
            return Ok(addComent);
        }
    }
}