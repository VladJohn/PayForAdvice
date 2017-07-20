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

        public AnswerModel UpdateRating (int idAnswer, int rating)
        {
            using (var uw = new UnitOfWork())
            {
                var answerRepo = uw.GetRepository<Answer>();
                var answerToUpdate = answerRepo.Find(idAnswer);
                answerToUpdate.Rating = rating;
                answerRepo.Update(answerToUpdate);
                uw.Save();
                return AnswerMapper.MapAnswer(answerToUpdate);
            }
        }
    }
}