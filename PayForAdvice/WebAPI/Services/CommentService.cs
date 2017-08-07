using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Mappings;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class CommentService
    {
        public CommentModel AddComment (CommentModel comment)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var commentRepository = unitOfWork.GetRepository<Comment>();
                var commentToAdd = CommentMapper.MapCommentDataModel(comment);
                commentToAdd.Date = DateTime.Now;
                commentRepository.Add(commentToAdd);
                unitOfWork.Save();
                return CommentMapper.MapComment(commentToAdd);
            }
        }

        public List<CommentModel> GetAllCommentsByQuestionId (int idQuestion)
        {
            var commentModels = new List<CommentModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var commentRepository = unitOfWork.GetRepository<Comment>();
                var commentList = commentRepository.GetAll();
                foreach (var comment in commentList)
                {
                    if(comment.QuestionId == idQuestion)
                    {
                        var commentModel = CommentMapper.MapComment(comment);
                        commentModels.Add(commentModel);
                    }
                }
            }
            return commentModels;
        }
    }
}