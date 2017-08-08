using System;
using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Validators;

namespace WebAPI.ValidatorsModel
{
    public class QuestionValidator : IValidatorModel<QuestionModel>
    {
        private List<string> errors = new List<string>();

        public List<string> Check(QuestionModel entity)
        {
            if (entity.QuestionText == null || entity.QuestionText == "")
            {
                errors.Add("Please enter a question");
            }
            if (entity.Order == null || entity.Order == "")
            {
                errors.Add("Please select a price");
            }
            return errors;
        }
    }
}