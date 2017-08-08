using Domain;
using Repository;
using System.Linq;
using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Validators;

namespace WebAPI.ValidatorsModel
{
    public class UserValidator : IValidatorModel<UserModelForSignUp>
    {
        private List<string> errors = new List<string>();

        public List<string> Check(UserModelForSignUp entity)
        {
            if (entity.Email == null || entity.Email == "")
            {
                errors.Add("Please enter an e-mail address");
            }
            if (entity.Name == null || entity.Name == "")
            {
                errors.Add("Please enter your name");
            }
            if (entity.Password == null || entity.Password == "" || entity.Password.Length<5)
            {
                errors.Add("Your password must be at least 5 characters long");
            }
            if (entity.Username == null || entity.Username == "")
            {
                errors.Add("Please enter an username");
            }
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                if (userRepository.GetAll().Any(user => user.Username == entity.Username))
                {
                    errors.Add("This username is taken. Please try another.");
                }
                if (userRepository.GetAll().Any(user => user.Email == entity.Email))
                {
                    errors.Add("This e-mail address is taken. Please try another.");
                }
            }
            return errors;
        }
    }
}