﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        [Required]
        public string QuestionText { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
    }
}
