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
    public class AnswerService
    {
        public AnswerModel Add(AnswerModel answer)
        {
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var answerToAdd = AnswerMapper.MapAnswerDataModel(answer);
                answerRepo.Add(answerToAdd);
                uw.Save();
                return AnswerMapper.MapAnswer(answerToAdd);
            }
        }

        public List<AnswerModel> GetAllAnswersByUserId(int id)
        {
            var result = new List<AnswerModel>();
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var answerList = answerRepo.GetAll();
                foreach (var ans in answerList)
                {
                    if (ans.UserId == id)
                    {
                        var a = AnswerMapper.MapAnswer(ans);
                        result.Add(a);
                    }
                }
            }
            return result;
        }

        public AnswerModel GetAnAnswerByQuestionId(int id)
        {
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var questionRepo = uw.GetRepository<Question>();
                var answerList = answerRepo.GetAll();
                var questionList = questionRepo.GetAll();
                foreach (var q in questionList)
                {
                    if (q.Id == id)
                    {
                        if (q.Status.Equals("solved"))
                        {
                            foreach (var ans in answerList)
                            {
                                if (ans.QuestionId == id)
                                {
                                    var a = AnswerMapper.MapAnswer(ans);
                                    return a;
                                }
                            }
                        }
                    }
                }
            }
            return null;
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
                answerToUpdate.Status = "reported";
                answerRepo.Update(answerToUpdate);
                uw.Save();
                return AnswerMapper.MapAnswer(answerToUpdate);
            }
        }
    }
}