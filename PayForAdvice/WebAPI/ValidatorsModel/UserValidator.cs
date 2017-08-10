using System;
using Domain;
using Repository;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebAPI.Models;
using WebAPI.Validators;

namespace WebAPI.ValidatorsModel
{
    public class UserValidator : IValidatorModel<UserModelForSignUp>
    {
        private List<string> errors = new List<string>();

        public List<string> Check(UserModelForSignUp entity)
        {
            const string matchUsernamePattern = @"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$";
            const string matchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                             + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                             + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
            const string matchNamePattern = @"^[a-z 'A-Z\s]+$";
            if (string.IsNullOrEmpty(entity.Email) || Regex.IsMatch(entity.Email, matchEmailPattern) == false )
            {
                errors.Add("Please enter a valid e-mail address");
            }
            if (string.IsNullOrEmpty(entity.Name) || Regex.IsMatch(entity.Name, matchNamePattern) == false || 
                entity.Name.IndexOf(" ") == entity.Name.Length - 1 || entity.Name.Contains(" ") != true )
            {
                errors.Add("Please enter a valid name");
            }
            if (string.IsNullOrEmpty(entity.Password) || entity.Password.Length<5)
            {
                errors.Add("Your password must be at least 5 characters long");
            }
            
            if (string.IsNullOrEmpty(entity.Username) || Regex.IsMatch(entity.Username, matchUsernamePattern) == false)
            {
                errors.Add("Please enter an valid username");
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