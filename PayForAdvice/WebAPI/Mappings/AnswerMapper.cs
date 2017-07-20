using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class AnswerMapper
    {
        public static AnswerModel MapAnswer(Answer answer)
        {
            return new AnswerModel { Id = answer.Id, AnswerText = answer.AnswerText, Date = answer.Date, Question = answer.Question, QuestionId = answer.QuestionId, Rating = answer.Rating, ReportText = answer.ReportText, Status = answer.Status, UserId = answer.UserId };
        }

        public static dynamic MapAnswerDataModel (AnswerModel answer)
        {
            return new  { Id = answer.Id, AnswerText = answer.AnswerText, Date = answer.Date, Question = answer.Question, QuestionId = answer.QuestionId, Rating = answer.Rating, ReportText = answer.ReportText, Status = answer.Status, UserId = answer.UserId};
        }
    }
}