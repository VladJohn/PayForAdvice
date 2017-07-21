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
    public class AnswerService
    {
        public List<AnswerModel> GetAllAnswersByUserId(int idUser)
        {
            var result = new List<AnswerModel>();
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var answerList = answerRepo.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.UserId == idUser)
                    {
                        var a = AnswerMapper.MapAnswer(answer);
                        result.Add(a);
                    }
                }
            }
            return result;
        }

        public List<AnswerModel> GetAllAnswersByUnsolvedReports()
        {
            var result = new List<AnswerModel>();
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var answerList = answerRepo.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.Status == "unsolved")
                    {
                        var a = AnswerMapper.MapAnswer(answer);
                        result.Add(a);
                    }
                }
            }
            return result;
        }

        public AnswerModel AddEmpty(int idQuestion, int idResponder)
        {
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var answer = new Answer { AnswerText = "", QuestionId = idQuestion, UserId = idResponder, Date = DateTime.Now, Status = "unreported"};
                answerRepo.Add(answer);
                uw.Save();
                return AnswerMapper.MapAnswer(answer);
            }
        }

        public AnswerModel GetAnAnswerByQuestionId(int idQuestion)
        {
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var questionRepo = uw.GetRepository<Question>();
                var answerList = answerRepo.GetAll();
                var questionList = questionRepo.GetAll();
                foreach (var question in questionList)
                {
                    if (question.Id == idQuestion)
                    {
                        if (question.Status.Equals("solved"))
                        {
                            foreach (var answer in answerList)
                            {
                                if (answer.QuestionId == idQuestion)
                                {
                                    var a = AnswerMapper.MapAnswer(answer);
                                    return a;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        public AnswerModel UpdateAnswer(AnswerModel answer)
        {
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var questionRepo = uw.GetRepository<Question>();
                var answerToUpdate = answerRepo.Find(answer.Id);
                var questionToUpdate = questionRepo.Find(answer.QuestionId);

                answerToUpdate.AnswerText = answer.AnswerText;
                answerToUpdate.Date = DateTime.Now;
                answerToUpdate.QuestionId = answer.QuestionId;
                answerToUpdate.UserId = answer.UserId;
                questionToUpdate.Status = "solved";

                answerRepo.Update(answerToUpdate);
                questionRepo.Update(questionToUpdate);
                uw.Save();
                return AnswerMapper.MapAnswer(answerToUpdate);
            }
        }

        public AnswerModel UpdateRating (AnswerModel answer)
        {
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var answerToUpdate = answerRepo.Find(answer.Id);
                answerToUpdate.Rating = answer.Rating;
                answerRepo.Update(answerToUpdate);
                uw.Save();
                return AnswerMapper.MapAnswer(answerToUpdate);
            }
        }

        public AnswerModel UpdateReport (AnswerModel answer)
        {
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var answerToUpdate = answerRepo.Find(answer.Id);
                answerToUpdate.ReportText = answer.ReportText;
                answerToUpdate.Status = "unsolved";
                answerRepo.Update(answerToUpdate);
                uw.Save();
                return AnswerMapper.MapAnswer(answerToUpdate);
            }
        }
    }
}