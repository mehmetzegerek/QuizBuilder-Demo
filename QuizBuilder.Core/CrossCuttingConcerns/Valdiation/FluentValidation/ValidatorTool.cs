using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBuilder.Core.CrossCuttingConcerns.Valdiation.FluentValidation
{
    public class ValidatorTool
    {
        public static void FluentValidate(IValidator validator,object entity)
        {
            var content = new ValidationContext<object>(entity);
            var result = validator.Validate(content);
            if (result.Errors.Count > 0)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
