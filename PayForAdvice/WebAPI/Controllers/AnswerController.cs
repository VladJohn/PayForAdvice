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
        //GET
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

        //GET
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

        public IHttpActionResult GetAnAnswerByQuestionIdPending(int idQuestionPending)
        {
            var service = new AnswerService();
            var answer = service.GetAnAnswerByQuestionIdPending(idQuestionPending);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        

        //GET
        public IHttpActionResult GetAllAnswersByUnsolvedReports()
        {
            var service = new AnswerService();
            var answers = service.GetAllAnswersByUnsolvedReports();
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
            var updateRating = service.UpdateRating(id, rating);
            if (updateRating == null)
            {
                return BadRequest();
            }
            return Ok(updateRating);
        }

        //PUT
        public IHttpActionResult PutReport(int id, string report)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var updateReport = service.UpdateReport(id, report);
            if (updateReport == null)
            {
                return BadRequest();
            }
            return Ok(updateReport);
        }

        [HttpPost]
        public async Task<UserId> post(int idAnswer)
        {
            AnswerService s = new AnswerService();
            var category = s.getCategoryByAnswerId(idAnswer);
            FacebookService fs = new FacebookService();
            return await fs.ShareAdviceGiven(category);
        }

        [HttpPost]
        public async Task<UserId> postRating(int rating)
        {
            FacebookService fs = new FacebookService();
            return await fs.ShareRatingGiven(rating);
        }

    }
}