using Domain;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAPI.Validators;

namespace WebAPI.Models
{
    [Validator(typeof(AnswerValidator))]
    public class AnswerModel
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string ReportText { get; set; }

        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}