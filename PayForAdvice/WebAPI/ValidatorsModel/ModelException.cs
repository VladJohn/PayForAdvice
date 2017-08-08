using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.ValidatorsModel
{
    public class ModelException : Exception
    {
        public ModelException(string message)
            : base(message) { }
    }
}