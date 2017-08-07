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
        public IHttpActionResult GetAllQuestionsByUserIdSortedByCriteria(int userIdForSorting, string sorter)
        {
            var service = new QuestionService();
            List<QuestionModel> questions = new List<QuestionModel>();
            if(sorter.Equals("normal"))
            {
                questions = service.GetAllQuestionsByUserId(userIdForSorting);
            }
            else
            {
                if(sorter.Equals("state"))
                {
                    questions = service.GetQuestionsByStatus(userIdForSorting);
                }
                else
                {
                    questions = service.GetQuestionsByDate(userIdForSorting);
                }
            }
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
        //POST
        public IHttpActionResult Add(QuestionModel question, int idResponder)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new QuestionService();
            var serviceAnswers = new AnswerService();
            var addQuestion = service.AddQuestion(question);
            serviceAnswers.AddEmptyAnswer(addQuestion.Id, idResponder);
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

        public IHttpActionResult GetAdvicerPendingQuestions(int idAdvicerForPending)
        {
            var service = new QuestionService();
            var questions = service.getAdvicerPendingQuestions(idAdvicerForPending);
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }


        //PUT
        public IHttpActionResult PutQuestion(int idQuestion)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new QuestionService();
            var updatedQuestion = service.MarkQuestionAsRefunded(idQuestion);
            if (updatedQuestion == null)
            {
                return BadRequest();
            }
            return Ok(updatedQuestion);
        }


    }
}