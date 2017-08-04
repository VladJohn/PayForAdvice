using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class QuestionMapper
    {
        public static QuestionModel MapQuestion (Question question)
        {
            return new QuestionModel { Id = question.Id, Date = Convert.ToDateTime(question.Date), QuestionText = question.QuestionText, Status = question.Status, UserId = question.UserId, Order=question.Order };
        }

        public static Question MapQuestionDataModel(QuestionModel question)
        {
            return new Question { Id = question.Id, UserId = question.UserId, Status = question.Status, QuestionText = question.QuestionText, Date = question.Date, Order=question.Order };
        }
    }
}