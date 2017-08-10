using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Validators;

namespace WebAPI.ValidatorsModel
{
    public class ReportValidator : IValidatorModel<AnswerModel>
    {
        private readonly List<string> _errors = new List<string>();

        public List<string> Check(AnswerModel entity)
        {
            if (string.IsNullOrEmpty(entity.ReportText))
            {
                _errors.Add("Please enter some details in your report");
            }
            return _errors;
        }
    }
}