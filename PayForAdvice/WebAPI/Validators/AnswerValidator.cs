using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class AnswerValidator : AbstractValidator<AnswerModel>
    {
        public AnswerValidator()
        {
            RuleFor(x => x.AnswerText).NotEmpty().WithMessage("Cannot submit an empty answer.");
            RuleFor(x => x.Rating).GreaterThan(5).WithMessage("The maximum rating is 5.")
                                  .LessThan(1).WithMessage("The minimum rating is 1.");
        }
    }
}