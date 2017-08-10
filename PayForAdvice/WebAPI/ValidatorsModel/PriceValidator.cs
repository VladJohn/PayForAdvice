using System.Collections.Generic;
using System.Globalization;
using WebAPI.Models;
using WebAPI.Validators;

namespace WebAPI.ValidatorsModel
{
    public class PriceValidator : IValidatorModel<PriceModel>
    {
        private readonly List<string> _errors = new List<string>();

        public List<string> Check(PriceModel entity)
        {
            double x;
            if (entity.Amount < 0 || double.TryParse(entity.Amount.ToString(CultureInfo.InvariantCulture), out x) != true)
            {
                _errors.Add("Please enter a valid price");
            }
            if (string.IsNullOrEmpty(entity.Details))
            {
                _errors.Add("Please enter some details");
            }
            if (entity.Amount == 0)
            {
                _errors.Add("Please enter a price");
            }
            return _errors;
        }
    }
}