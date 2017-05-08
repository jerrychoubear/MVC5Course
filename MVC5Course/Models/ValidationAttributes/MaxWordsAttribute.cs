using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttributes
{
    public class MaxWordsAttribute : ValidationAttribute
    {
        public MaxWordsAttribute(int maxWords) : base("{0} has too many words.")
        {
            _maxWords = maxWords;
        }

        private readonly int _maxWords;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;

            if (str != null && str.Length > _maxWords)
            {
                return new ValidationResult("ValidationAttribute triggers! Too much words!", new string[] { "ProductName" });
            }

            return ValidationResult.Success;
        }
    }
}