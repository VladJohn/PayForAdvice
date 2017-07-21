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
        public IHttpActionResult PutAnswer([FromBody]AnswerModel ans)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var ac = service.UpdateAnswer(ans);
            if (ac == null)
            {
                return BadRequest();
            }
            return Ok(ac);
        }

        //PUT
        public IHttpActionResult PutRating([FromBody]AnswerModel ans)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var ac = service.UpdateRating(ans);
            if (ac == null)
            {
                return BadRequest();
            }
            return Ok(ac);
        }

        //PUT
        public IHttpActionResult PutReport([FromBody]AnswerModel ans)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var service = new AnswerService();
            var ac = service.UpdateReport(ans);
            if (ac == null)
            {
                return BadRequest();
            }
            return Ok(ac);
        }
    }
}