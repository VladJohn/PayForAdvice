using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class QuestionController : ApiController
    {
        //GET
        public IHttpActionResult GetAllQuestionsByUserId(int userId)
        {
            var service = new QuestionService();
            var questions = service.GetAllQuestionsByUserId(userId);
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        //POST
        public IHttpActionResult Add([FromBody]QuestionModel question, int idResponder)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new QuestionService();
            var serviceAnswers = new AnswerService();
            var addQuestion = service.Add(question);
            serviceAnswers.AddEmpty(addQuestion.Id, idResponder);
            if (addQuestion == null)
            {
                return BadRequest();
            }
            return Ok(addQuestion);
        }
    }
}