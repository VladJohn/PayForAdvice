using System;
using System.Collections.Generic;

namespace Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public int UserIdSender { get; set; }
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