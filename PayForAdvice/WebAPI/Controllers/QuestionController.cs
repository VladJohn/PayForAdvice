using System.Collections.Generic;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Services;
using Domain.Enums;

namespace WebAPI.Controllers
{
    public class QuestionController : ApiController
    {
        //GET all the questions for a user giving his id(userId)
        //if there are not questions it returns NotFound
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

        //GET the questions for a user, sorting them by Criteria
        //if there are not questions it returns NotFound
        public IHttpActionResult GetAllQuestionsByUserIdSortedByCriteria(int userIdForSorting, int sorter)
        {
            var service = new QuestionService();
            List<QuestionModel> questions;
            if(sorter == (int)QuestionSorterEnum.Normal)
                questions = service.GetAllQuestionsByUserId(userIdForSorting);
            else
                questions = sorter == (int)QuestionSorterEnum.Status ? service.GetQuestionsByStatus(userIdForSorting) : service.GetQuestionsByDate(userIdForSorting);

            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        //GET the question with the id = idQuestion
        //if it doesn't exist return NotFound()
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

        //POST- add a new question to the question list
        public IHttpActionResult Add(QuestionModel question, int idResponder)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new QuestionService();
            var serviceAnswers = new AnswerService();
            var addQuestion = service.AddQuestion(question);
            serviceAnswers.AddEmptyAnswer(addQuestion.Id, idResponder);
            return Ok(addQuestion);
        }

        //GET all the questions for which the advicer gave an answer
        public IHttpActionResult GetAdvicerAnsweredQuestions(int idAdvicer)
        {
            var service = new QuestionService();
            var questions = service.GetAdvicerAnsweredQuestions(idAdvicer);
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        //GET all the questions of an advicer with an answer pending
        public IHttpActionResult GetAdvicerPendingQuestions(int idAdvicerForPending)
        {
            var service = new QuestionService();
            var questions = service.GetAdvicerPendingQuestions(idAdvicerForPending);
            if (questions == null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        //PUT - update a question
        //if there is an error it returns BadRequest
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