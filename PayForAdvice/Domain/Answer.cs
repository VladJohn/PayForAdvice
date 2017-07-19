using System;
using System.Collections.Generic;

namespace Domain
{
    public class Answer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string ReportText { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int UserIdResponder { get; set; }
        public User UserResponder { get; set; }

        public ICollection<Upload> Uploads { get; set; }

        public Answer()
        {
            Uploads = new HashSet<Upload>();
        }
    }
}