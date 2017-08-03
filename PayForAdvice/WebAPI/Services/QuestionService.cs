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

        public List<QuestionModel> GetQuestionsByStatus(int idStatus)
        {
            var result = new List<QuestionModel>();
            using (var uw = new UnitOfWork())
            {
                var questionRepo = uw.GetRepository<Question>();
                var questionList = questionRepo.GetAll();
                foreach (var question in questionList)
                {
                    if (question.UserId == idStatus)
                    {
                        var q = QuestionMapper.MapQuestion(question);
                        result.Add(q);
                    }
                }
                result = result.OrderBy(x => x.Status).ToList();
            }
            return result;
        }

        public List<QuestionModel> GetQuestionsByDate(int idDate)
        {
            var result = new List<QuestionModel>();
            using (var uw = new UnitOfWork())
            {
                var questionRepo = uw.GetRepository<Question>();
                var questionList = questionRepo.GetAll();
                foreach (var question in questionList)
                {
                    if (question.UserId == idDate)
                    {
                        var q = QuestionMapper.MapQuestion(question);
                        result.Add(q);
                    }
                }
                result = result.OrderByDescending(x => x.Date).ToList();
            }
            return result;
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

        public QuestionModel GetQuestionById(int idQuestion)
        {
            var result = new QuestionModel();
            using (var uw = new UnitOfWork())
            {
                var questionRepo = uw.GetRepository<Question>();
                var questionList = questionRepo.GetAll();
                foreach (var question in questionList)
                {
                    if (question.Id == idQuestion)
                    {
                        var q = QuestionMapper.MapQuestion(question);
                        return q;
                    }
                }
            }
            return null;
        }

        public List<QuestionModel> getAdvicerAnsweredQuestions(int idAdvicer)
        {
            var result = new List<QuestionModel>();
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var questionRepo = uw.GetRepository<Question>();
                var answerList = answerRepo.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.UserId == idAdvicer)
                    {
                        var found = questionRepo.GetAll().ToList().Where(x => x.Id.Equals(answer.QuestionId) && x.Status=="solved").ToList();
                        foreach (var question in found)
                        {
                            var a = QuestionMapper.MapQuestion(question);
                            result.Add(a);
                        }
                    }
                }
            }
            return result;
        }

        public List<QuestionModel> getAdvicerPendingQuestions(int idAdvicer)
        {
            var result = new List<QuestionModel>();
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var questionRepo = uw.GetRepository<Question>();
                var answerList = answerRepo.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.UserId == idAdvicer)
                    {
                        var found = questionRepo.GetAll().ToList().Where(x => x.Id.Equals(answer.QuestionId) && x.Status == "pending").ToList();
                        foreach (var question in found)
                        {
                            var a = QuestionMapper.MapQuestion(question);
                            result.Add(a);
                        }
                    }
                }
            }
            return result;
        }

    }
}