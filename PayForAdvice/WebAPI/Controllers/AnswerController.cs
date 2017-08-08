using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPI.FacebookIntegration.Models;
using WebAPI.FacebookIntegration.Service;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class AnswerController : ApiController
    {
        //GET all the answers for an user given by id = idUser
        public IHttpActionResult GetAllAnswersByUserId(int idUser)
        {
            var service = new AnswerService();
            var answers = service.GetAllAnswersByUserId(idUser);
            if (answers == null)
            {
                return NotFound();
            }
            return Ok(answers);
        }

        //GET all the answers for a question given by id = idQuestion
        public IHttpActionResult GetAnswerByQuestionId(int idQuestion)
        {
            var service = new AnswerService();
            var answer = service.GetAnAnswerByQuestionId(idQuestion);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        //GET a pending answer for a question given by id = idQuestionPending
        public IHttpActionResult GetAPendingAnswerByQuestionId(int idQuestionPending)
        {
            var service = new AnswerService();
            var answer = service.GetAPendingAnswerByQuestionId(idQuestionPending);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        

        //GET
        public IHttpActionResult GetAllAnswersWithUnsolvedReports()
        {
            var service = new AnswerService();
            var answers = service.GetAllAnswersWithUnsolvedReports();
            if (answers == null)
            {
                return NotFound();
            }
            return Ok(answers);
        }

        //PUT
        public IHttpActionResult PutAnswer(AnswerModel answer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var updateAnswer = service.UpdateAnswer(answer);
            if (updateAnswer == null)
            {
                return BadRequest();
            }
            return Ok(updateAnswer);
        }

        //PUT
        public IHttpActionResult PutRating(int id, string rating)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var updatedAnswer = service.UpdateRating(id, rating);
            if (updatedAnswer == null)
            {
                return BadRequest();
            }
            return Ok(updatedAnswer);
        }

        //PUT
        public IHttpActionResult PutReport(int id, string report)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var updatedAnswer = service.UpdateReport(id, report);
            if (updatedAnswer == null)
            {
                return BadRequest();
            }
            return Ok(updatedAnswer);
        }

        [HttpPost]
        public async Task<UserId> PostRecievedAnswerOnFacebook(int idAnswer)
        {
            AnswerService service = new AnswerService();
            var category = service.GetCategoryByAnswerId(idAnswer);
            FacebookService facebookService = new FacebookService();
            return await facebookService.ShareAdviceGiven(category);
        }

        [HttpPost]
        public async Task<UserId> PostReceivedRatingOnFacebook(int rating)
        {
            FacebookService facebookService = new FacebookService();
            return await facebookService.ShareRatingGiven(rating);
        }

    }
}