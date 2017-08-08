using Domain;
using Domain.Enums;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Mappings;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class AnswerService
    {
        //get all the answers for a user with the id = idUser
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

        //return a list with all the answers for a user that don't have a report that isn't solved
        public List<AnswerModel> GetAllAnswersWithUnsolvedReports()
        {
            var result = new List<AnswerModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var answerList = answerRepository.GetAll();
                foreach (var answer in answerList)
                {
                    if (answer.Status != (int) AnswerStatusEnum.Unsolved) continue;
                    var a = AnswerMapper.MapAnswer(answer);
                    result.Add(a);
                }
            }
            return result;
        }

        //create an answer entity that has date, id of it's question and the responder id
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

        //return the answer for a question with the id = idQuestion
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

        //return the answer that has the status  = pending and the question id = idQuestion 
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

        //update the answer for a certain question
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

        //update the rating for a certain answer given by id = idAnswer
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

        //update the report for an answer given by id = idAnswer
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

        //return the category of an answer guven by id = idAnswer
        public string GetCategoryByAnswerId(int idAnswer)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var answerRepository = unitOfWork.GetRepository<Answer>();
                var userRepository = unitOfWork.GetRepository<User>();
                foreach (var answer in answerRepository.GetAll())
                {
                    if (answer.Id != idAnswer) continue;
                    foreach(var user in userRepository.GetAll())
                    {
                        if(answer.UserId == user.Id)
                        {
                            return user.Categories.FirstOrDefault()?.Name;
                        }
                    }
                }
            }
            return "";
        }
    }
}