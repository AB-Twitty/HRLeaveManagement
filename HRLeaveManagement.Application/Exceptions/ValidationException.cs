using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace HRLeaveManagement.Application.Exceptions
{
	public class ValidationException : ApplicationException
	{
        public List<string> Errors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            foreach(var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}
