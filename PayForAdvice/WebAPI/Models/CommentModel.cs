using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        [Required]
        public string CommentText { get; set; }
        public int SenderId { get; set; }
        public DateTime Date { get; set; }

        public int QuestionId { get; set; }

    }
}