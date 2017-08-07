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
        //add a question to the list of questions
        public QuestionModel AddQuestion (QuestionModel question)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var questionRepository = unitOfWork.GetRepository<Question>();
                var questionToAdd = QuestionMapper.MapQuestionDataModel(question);
                questionToAdd.Date = DateTime.Now;
                questionToAdd.Status = "pending";
                questionRepository.Add(questionToAdd);
                unitOfWork.Save();
                return QuestionMapper.MapQuestion(questionToAdd);
            }
        }

        //it will return a list with all the questions that have a certain statusId
        public List<QuestionModel> GetQuestionsByStatus(int idStatus)
        {
            var result = new List<QuestionModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var questionRepository = unitOfWork.GetRepository<Question>();
                var questionList = questionRepository.GetAll();
                foreach (var question in questionList)
                {
                    if (question.UserId == idStatus)
                    {
                        var questionToAdd= QuestionMapper.MapQuestion(question);
                        result.Add(questionToAdd);
                    }
                }
                result = result.OrderBy(x => x.Status).ToList();
            }
            return result;
        }

        //it will return a list with all the questions ordered descending by date(newer to older)
        public List<QuestionModel> GetQuestionsByDate(int idDate)
        {
            var result = new List<QuestionModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var questionRepository = unitOfWork.GetRepository<Question>();
                var questionList = questionRepository.GetAll();
                foreach (var question in questionList)
                {
                    if (question.UserId == idDate)
                    {
                        var questionToAdd= QuestionMapper.MapQuestion(question);
                        result.Add(questionToAdd);
                    }
                }
                result = result.OrderByDescending(x => x.Date).ToList();
            }
            return result;
        }

        //it returns a list of Questions for a certain userId
        public List<QuestionModel> GetAllQuestionsByUserId (int idUser)
        {
            var result = new List<QuestionModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var questionRepository = unitOfWork.GetRepository<Question>();
                var questionList = questionRepository.GetAll();
                foreach (var question in questionList)
                {
                    if (question.UserId == idUser)
                    {
                        var questionToAdd= QuestionMapper.MapQuestion(question);
                        result.Add(questionToAdd);
                    }
                }
            }
            return result;
        }

        //returns the question with the id = idQuestion
        public QuestionModel GetQuestionById(int idQuestion)
        {
            var result = new QuestionModel();
            using (var unitOfWork = new UnitOfWork())
            {
                var questionRepository = unitOfWork.GetRepository<Question>();
                var questionList = questionRepository.GetAll();
                foreach (var question in questionList)
                {
                    if (question.Id == idQuestion)
                    {
                        var questionToAdd= QuestionMapper.MapQuestion(question);
                        return questionToAdd;
                    }
                }
            }
            return null;
        }

        //returns a list with all the answered questions for an advicer
        public List<QuestionModel> getAdvicerAnsweredQuestions(int idAdvicer)
        {
            var result = new List<QuestionModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepo = unitOfWork.GetRepository<Answer>();
                var questionRepository = unitOfWork.GetRepository<Question>();
                var answerList = answerRepo.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.UserId == idAdvicer)
                    {
                        var found = questionRepository.GetAll().ToList().Where(x => x.Id.Equals(answer.QuestionId) && x.Status=="solved").ToList();
                        foreach (var question in found)
                        {
                            var questionToAdd = QuestionMapper.MapQuestion(question);
                            result.Add(questionToAdd);
                        }
                    }
                }
            }
            return result;
        }

        //returns a list with all the pending questions for an advicer
        public List<QuestionModel> getAdvicerPendingQuestions(int idAdvicer)
        {
            var result = new List<QuestionModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepo = unitOfWork.GetRepository<Answer>();
                var questionRepository = unitOfWork.GetRepository<Question>();
                var answerList = answerRepo.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.UserId == idAdvicer)
                    {
                        var found = questionRepository.GetAll().ToList().Where(x => x.Id.Equals(answer.QuestionId) && x.Status == "pending").ToList();
                        foreach (var question in found)
                        {
                            var questionToAdd = QuestionMapper.MapQuestion(question);
                            result.Add(questionToAdd);
                        }
                    }
                }
            }
            return result;
        }

        //it will change the status of an question as refunded
        public QuestionModel MarkQuestionAsRefunded(int idQuestion)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var questionRepository = unitOfWork.GetRepository<Question>();
                var question = questionRepository.Find(idQuestion);
                question.Status = "refunded";
                questionRepository.Update(question);
                unitOfWork.Save();
                return QuestionMapper.MapQuestion(question);
            }
            
        }

    }
}