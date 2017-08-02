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

        //GET
        public IHttpActionResult GetQuestionById(int idQuestion)
        {
            var service = new QuestionService();
            var questions = service.GetQuestionById(idQuestion);
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        //sort the questions by the status
        public IHttpActionResult GetQuestionsByStatus(int IdUser)
        {
            var service = new QuestionService();
            var questions = service.GetQuestionsByStatus(IdUser);
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }
        //sort the questions by date
        public IHttpActionResult GetQuestionsByDate(int IdUsers)
        {
            var service = new QuestionService();
            var questions = service.GetQuestionsByDate(IdUsers);
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }
        //POST
        public IHttpActionResult Add(QuestionModel question, int idResponder)
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

        public IHttpActionResult GetAdvicerAnsweredQuestions(int idAdvicer)
        {
            var service = new QuestionService();
            var questions = service.getAdvicerAnsweredQuestions(idAdvicer);
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }
    }
}