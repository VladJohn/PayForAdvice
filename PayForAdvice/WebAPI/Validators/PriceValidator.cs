using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class PriceValidator : AbstractValidator<PriceModel>
    {
        public PriceValidator()
        {
           // RuleFor(x => x.Amount).LessThan(0).WithMessage("Services can't be cheaper than Free. You can't pay users for the advice you give them.");
           // RuleFor(x => x.Details).Length(10, 50).WithMessage("Keep the details short and on point.");
        }
    }
}