﻿using System.ComponentModel.DataAnnotations;

namespace ASPCollegeBooking.Models
{
    public class ValidateRoom : ValidationAttribute
    {

        //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-2.0
        //https://github.com/aspnet/Docs/blob/master/aspnetcore/mvc/models/validation/sample/Views/Movies/Create.cshtml

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as Events;

            if (model.ResourceId.Contains("Please"))
                return new ValidationResult(GetErrorMessage(validationContext));

            //if (model.Start > model.End)
            //    return new ValidationResult(GetErrorMessage(validationContext));

            return ValidationResult.Success;
        }

        private string GetErrorMessage(ValidationContext validationContext)
        {
            // Message that was supplied
            if (!string.IsNullOrEmpty(this.ErrorMessage))
                return this.ErrorMessage;

            // Use generic message: i.e. The field {0} is invalid
            //return this.FormatErrorMessage(validationContext.DisplayName);

            // Custom message
            return $"{validationContext.DisplayName} Start date must be before End date Validation in Datecheck class";
        }
    }
}
