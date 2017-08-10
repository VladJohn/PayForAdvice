using Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebAPI.Models;
using WebAPI.Validators;

namespace WebAPI.ValidatorsModel
{
    public class AdviserValidator : IValidatorModel<UserModelForSignUpAdviser>
    {
        private List<string> errors = new List<string>();

        public List<string> Check(UserModelForSignUpAdviser entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                errors.Add("Please enter your name");
            }
            if (entity.Name.Length < 5 || entity.Name.Contains(" ") != true || entity.Name.IndexOf(" ") == entity.Name.Length - 1)
            {
                errors.Add("Please enter a correct name");
            }
            if (string.IsNullOrEmpty(entity.Email))
            {
                errors.Add("Please enter an email adress");
            }
            const string matchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                             + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                             + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
            if (Regex.IsMatch(entity.Email, matchEmailPattern) == false)
            {
                errors.Add("Please enter a valid email adress");
            }
            if (string.IsNullOrEmpty(entity.Password))
            {
                errors.Add("Please enter a password");
            }
            else if (entity.Password.Length < 5)
            {
                errors.Add("Please enter a longer password");
            }
            if (entity.CategoryId <= 0)
            {
                errors.Add("Please choose a field");
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