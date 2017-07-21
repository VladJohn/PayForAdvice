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
    public class QuestionService
    {
        public QuestionModel Add (QuestionModel question)
        {
            using (var uw = new UnitOfWork())
            {
                var questionRepo = uw.GetRepository<Question>();
                var questionToAdd = QuestionMapper.MapQuestionDataModel(question);
                questionToAdd.Date = DateTime.Now;
                questionToAdd.Status = "pending";
                questionRepo.Add(questionToAdd);
                uw.Save();
                return QuestionMapper.MapQuestion(questionToAdd);
            }
        }

        public List<QuestionModel> GetAllQuestionsByUserId (int idUser)
        {
            var result = new List<QuestionModel>();
            using (var uw = new UnitOfWork())
            {
                var questionRepo = uw.GetRepository<Question>();
                var questionList = questionRepo.GetAll();
                foreach (var question in questionList)
                {
                    if (question.UserId == idUser)
                    {
                        var q = QuestionMapper.MapQuestion(question);
                        result.Add(q);
                    }
                }
            }
            return result;
        }
    }
}