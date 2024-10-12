using System.ComponentModel.DataAnnotations;

namespace ProSoft.EF.ValidationAttributes
{
    public class ReqiureToAttribute : ValidationAttribute
    {
        private readonly string _propertyName;
        public ReqiureToAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_propertyName);
            if (property == null) {
                return new ValidationResult($"'{_propertyName}' does not exist.");
            }

            var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
            if (value == null && propertyValue != null) {
                var errorMessage = string.IsNullOrEmpty(ErrorMessage) ? $"{validationContext.DisplayName} is required when {_propertyName} is provided." : ErrorMessage;
                return new ValidationResult(errorMessage);
            }
            
            return ValidationResult.Success;
        }
    }
}
