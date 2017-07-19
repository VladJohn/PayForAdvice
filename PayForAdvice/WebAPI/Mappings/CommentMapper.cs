using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Mappings
{
    public class CommentMapper
    {
        public static CommentModel MapComment (Comment comment)
        {
            return new CommentModel { Id = comment.Id, CommentText = comment.CommentText, QuestionId = comment.QuestionId, SenderId = comment.SenderId };
        }

        public static Comment MapCommentDataModel (CommentModel comment)
        {
            return new Comment { Id = comment.Id, SenderId = comment.SenderId, CommentText = comment.CommentText, QuestionId = comment.QuestionId, Date = comment.Date };
        }
    }
}