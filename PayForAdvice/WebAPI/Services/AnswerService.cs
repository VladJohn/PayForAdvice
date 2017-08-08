using Domain;
using Domain.Enums;
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
            var answerModels = new List<AnswerModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var answerList = answerRepository.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.UserId == idUser)
                    {
                        var answerModel = AnswerMapper.MapAnswer(answer);
                        answerModels.Add(answerModel);
                    }
                }
            }
            return answerModels;
        }

        public List<AnswerModel> GetAllAnswersWithUnsolvedReports()
        {
            var result = new List<AnswerModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var answerList = answerRepository.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.Status == (int)AnswerStatusEnum.Unsolved)
                    {
                        var a = AnswerMapper.MapAnswer(answer);
                        result.Add(a);
                    }
                }
            }
            return result;
        }

        public AnswerModel AddEmptyAnswer(int idQuestion, int idResponder)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var answer = new Answer { AnswerText = "", QuestionId = idQuestion, UserId = idResponder, Date = DateTime.Now, Status = (int)AnswerStatusEnum.Unsolved };
                answerRepository.Add(answer);
                unitOfWork.Save();
                return AnswerMapper.MapAnswer(answer);
            }
        }

        public AnswerModel GetAnAnswerByQuestionId(int idQuestion)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var questionRepo = unitOfWork.GetRepository<Question>();
                var answerList = answerRepository.GetAll();
                var questionList = questionRepo.GetAll();
                foreach (var question in questionList)
                {
                    if (question.Id == idQuestion)
                    {
                        foreach (var answer in answerList)
                        {
                            if (answer.QuestionId == idQuestion)
                            {
                                var answerModel = AnswerMapper.MapAnswer(answer);
                                return answerModel;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public AnswerModel GetAPendingAnswerByQuestionId(int idQuestion)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var questionRepository = unitOfWork.GetRepository<Question>();
                var answerList = answerRepository.GetAll();
                var questionList = questionRepository.GetAll();
                foreach (var question in questionList)
                {
                    if (question.Id == idQuestion && question.Status == (int)QuestionStatusEnum.Pending)
                    {
                        foreach (var answer in answerList)
                        {
                            if (answer.QuestionId == idQuestion)
                            {
                                var answerModel = AnswerMapper.MapAnswer(answer);
                                return answerModel;
                            }
                        }
                    }
                    
                }
            }
            return null;
        }

        public AnswerModel UpdateAnswer(AnswerModel answer)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var questionRepository = unitOfWork.GetRepository<Question>();
                var answerToUpdate = answerRepository.Find(answer.Id);
                var questionToUpdate = questionRepository.Find(answerToUpdate.QuestionId);

                answerToUpdate.AnswerText = answer.AnswerText;
                answerToUpdate.Date = DateTime.Now;
                questionToUpdate.Status = (int)QuestionStatusEnum.Solved;

                answerRepository.Update(answerToUpdate);
                questionRepository.Update(questionToUpdate);
                unitOfWork.Save();
                return AnswerMapper.MapAnswer(answerToUpdate);
            }
        }

        public AnswerModel UpdateRating (int idAnswer, string rating)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var answerToUpdate = answerRepository.Find(idAnswer);
                answerToUpdate.Rating = Int32.Parse(rating);
                answerRepository.Update(answerToUpdate);
                unitOfWork.Save();
                return AnswerMapper.MapAnswer(answerToUpdate);
            }
        }

        public AnswerModel UpdateReport (int idAnswer, string report)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var answerToUpdate = answerRepository.Find(idAnswer);
                answerToUpdate.ReportText = report;
                answerToUpdate.Status = (int)AnswerStatusEnum.Reported;
                answerRepository.Update(answerToUpdate);
                unitOfWork.Save();
                return AnswerMapper.MapAnswer(answerToUpdate);
            }
        }

        public string GetCategoryByAnswerId(int idAnswer)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var userRepository = unitOfWork.GetRepository<User>();
                foreach (var answer in answerRepository.GetAll())
                {
                    if (answer.Id == idAnswer)
                    {
                        foreach(var user in userRepository.GetAll())
                        {
                            if(answer.UserId == user.Id)
                            {
                                return user.Categories.FirstOrDefault().Name;
                            }
                        }
                    }
                }
            }
            return "";
        }
    }
}