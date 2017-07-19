﻿using System;
using System.Collections.Generic;

namespace Domain
{
    public class Question : Idable
    {
        public string QuestionText { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public int UserId { get; set; }
        public User UserSender { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public Question()
        {
            Comments = new HashSet<Comment>();
            Answers = new HashSet<Answer>();
        }
    }
}