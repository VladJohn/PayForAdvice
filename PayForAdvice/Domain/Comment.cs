using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment : Idable
    {
        public string CommentText { get; set; }
        public int SenderId { get; set; }
        public DateTime Date { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }


    }
}
