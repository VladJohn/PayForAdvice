using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using WebAPI.Mappings;
using WebAPI.Models;
using WebAPI.ValidatorsModel;

namespace WebAPI.Services
{
    public class QuestionService
    {
        private QuestionValidator validator = new QuestionValidator();

        //add a question to the list of questions
        public QuestionModel AddQuestion (QuestionModel question)
        {
            var errors = validator.Check(question);
            if (errors.Count!=0)
            {
                throw new ModelException(string.Join(Environment.NewLine, errors));
            }
            using (var unitOfWork = new UnitOfWork())
            {
                var questionRepository = unitOfWork.GetRepository<Question>();
                var questionToAdd = QuestionMapper.MapQuestionDataModel(question);
                questionToAdd.Date = DateTime.Now;
                questionToAdd.Status = (int)QuestionStatusEnum.Pending;
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
                    if (question.UserId != idStatus) continue;
                    var questionToAdd = QuestionMapper.MapQuestion(question);
                    result.Add(questionToAdd);
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
                    if (question.UserId != idDate) continue;
                    var questionToAdd= QuestionMapper.MapQuestion(question);
                    result.Add(questionToAdd);
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
                    if (question.UserId != idUser) continue;
                    var questionToAdd= QuestionMapper.MapQuestion(question);
                    result.Add(questionToAdd);
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
                    if (question.Id != idQuestion) continue;
                    var questionToAdd= QuestionMapper.MapQuestion(question);
                    return questionToAdd;
                }
            }
            return null;
        }

        //returns a list with all the answered questions for an advicer
        public List<QuestionModel> GetAdvicerAnsweredQuestions(int idAdvicer)
        {
            var result = new List<QuestionModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepo = unitOfWork.GetRepository<Answer>();
                var questionRepository = unitOfWork.GetRepository<Question>();
                var answerList = answerRepo.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.UserId != idAdvicer) continue;
                    var found = questionRepository.GetAll().ToList().Where(x => x.Id.Equals(answer.QuestionId) && x.Status == (int)QuestionStatusEnum.Solved).ToList();
                    result.AddRange(found.Select(QuestionMapper.MapQuestion));
                }
            }
            return result;
        }

        //returns a list with all the pending questions for an advicer
        public List<QuestionModel> GetAdvicerPendingQuestions(int idAdvicer)
        {
            var result = new List<QuestionModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepo = unitOfWork.GetRepository<Answer>();
                var questionRepository = unitOfWork.GetRepository<Question>();
                var answerList = answerRepo.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.UserId != idAdvicer) continue;
                    var found = questionRepository.GetAll().ToList().Where(x => x.Id.Equals(answer.QuestionId) && x.Status == (int)QuestionStatusEnum.Pending).ToList();
                    result.AddRange(found.Select(QuestionMapper.MapQuestion));
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
                question.Status = (int)QuestionStatusEnum.Refunded;
                questionRepository.Update(question);
                unitOfWork.Save();
                return QuestionMapper.MapQuestion(question);
            }
            
        }

    }
}