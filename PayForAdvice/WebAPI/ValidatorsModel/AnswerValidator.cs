using System.Collections.Generic;
using WebAPI.Validators;
using WebAPI.Models;

namespace WebAPI.ValidatorsModel
{
    public class AnswerValidator : IValidatorModel<AnswerModel>
    {
        private List<string> errors = new List<string>();

        public List<string> Check(AnswerModel entity)
        {
            if (entity.AnswerText == null || entity.AnswerText == "")
            {
                errors.Add("Please enter a question");
            }
            return errors;
        }
    }
}