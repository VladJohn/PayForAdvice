﻿using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class AnswerModel
    {
        public int Id { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string ReportText { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int UserId { get; set; }
    }
}