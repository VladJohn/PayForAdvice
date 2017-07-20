﻿using Domain;
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
        public CommentModel Add (CommentModel comment)
        {
            using (var uw = new UnitOfWork())
            {
                var commentRepo = uw.GetRepository<Comment>();
                var commentToAdd = CommentMapper.MapCommentDataModel(comment);
                commentRepo.Add(commentToAdd);
                uw.Save();
                return CommentMapper.MapComment(commentToAdd);
            }
        }

        public List<CommentModel> GetAllCommentsForQuestionId (int id)
        {
            var result = new List<CommentModel>();
            using (var uw = new UnitOfWork())
            {
                var commentRepo = uw.GetRepository<Comment>();
                var commentList = commentRepo.GetAll();
                foreach (var comm in commentList)
                {
                    if(comm.Id == id)
                    {
                        var c = CommentMapper.MapComment(comm);
                        result.Add(c);
                    }
                }
            }
            return result;
        }
    }
}