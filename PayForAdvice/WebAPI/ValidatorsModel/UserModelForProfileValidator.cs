using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebAPI.Models;
using WebAPI.Validators;

namespace WebAPI.ValidatorsModel
{
    public class UserModelForProfileValidator : IValidatorModel<UserModelForProfile>
    {
        private readonly List<string> _errors = new List<string>();

        public List<string> Check(UserModelForProfile entity)
        {
            if (string.IsNullOrEmpty(entity.Name))
            {
                _errors.Add("Please enter your name");
            }
            if (entity.Name.Length < 5 || entity.Name.Contains(" ") != true)
            {
                _errors.Add("Please enter a correct name");
            }
            if (string.IsNullOrEmpty(entity.Email))
            {
                _errors.Add("Please enter an email adress");
            }
            const string matchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                             + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                             + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                             + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";
            if (Regex.IsMatch(entity.Email, matchEmailPattern) == false)
            {
                _errors.Add("Please enter a valid email adress");
            }
            if (string.IsNullOrEmpty(entity.Password))
            {
                _errors.Add("Please enter a password");
            }
            else if (entity.Password.Length < 5)
            {
                _errors.Add("Please enter a longer password");
            }     
            return _errors;
        }
    }
}