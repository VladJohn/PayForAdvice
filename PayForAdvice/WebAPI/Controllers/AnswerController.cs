using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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
        public IHttpActionResult PutRating(AnswerModel answer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var updateRating = service.UpdateRating(answer);
            if (updateRating == null)
            {
                return BadRequest();
            }
            return Ok(updateRating);
        }

        //PUT
        public IHttpActionResult PutReport(AnswerModel answer)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var updateReport = service.UpdateReport(answer);
            if (updateReport == null)
            {
                return BadRequest();
            }
            return Ok(updateReport);
        }
    }
}