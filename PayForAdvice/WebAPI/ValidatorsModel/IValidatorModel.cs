using System.Collections.Generic;

namespace WebAPI.Validators
{
    public interface IValidatorModel<T>
    {
        List<string> Check(T entity);
    }
}
