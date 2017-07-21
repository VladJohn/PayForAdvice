using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryModel>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).Length(2, 100).WithMessage("Name of category is too long. Must be between 2 and 100 characters.");
        }
    }
}