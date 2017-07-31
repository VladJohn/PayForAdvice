using Domain;
using FluentValidation;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Validators
{
    public class UserValidator : AbstractValidator<UserModelForSignUp>
    {
        public UserValidator()
        {
            using (var uw = new UnitOfWork())
            {
                var userRepo = uw.GetRepository<User>();
                var users = userRepo.GetAll();
                foreach(var user in users)
                {
                    //RuleFor(x => x.Username).Equal(user.Username).WithMessage("Username already in use.");
                    //RuleFor(x => x.Password).Length(6, 40).WithMessage("Password too short.");
                    //RuleFor(x => x.Email).Equal(user.Email).WithMessage("Email already in use.");
                }
            }
        }
    }
}