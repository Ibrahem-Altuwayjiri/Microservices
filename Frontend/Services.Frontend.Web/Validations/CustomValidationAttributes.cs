using System.ComponentModel.DataAnnotations;

namespace Services.Frontend.Web.Validations
{
    /// <summary>
    /// Custom validation attribute for validating names (both Arabic and English)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidNameAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;
        private readonly string _fieldName;
        private readonly bool _allowNumbers;
        private readonly bool _allowSpecialChars;

        public ValidNameAttribute(int minLength = 2, int maxLength = 200, string fieldName = "Name", 
            bool allowNumbers = true, bool allowSpecialChars = false)
        {
            _minLength = minLength;
            _maxLength = maxLength;
            _fieldName = fieldName;
            _allowNumbers = allowNumbers;
            _allowSpecialChars = allowSpecialChars;
            ErrorMessage = $"{{0}} must be between {minLength} and {maxLength} characters";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var stringValue = value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue))
                return new ValidationResult($"{_fieldName} cannot be empty");

            if (stringValue.Length < _minLength || stringValue.Length > _maxLength)
                return new ValidationResult($"{_fieldName} must be between {_minLength} and {_maxLength} characters");

            // Check for valid characters
            if (!_allowNumbers && stringValue.Any(char.IsDigit))
                return new ValidationResult($"{_fieldName} cannot contain numbers");

            if (!_allowSpecialChars && !stringValue.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)))
                return new ValidationResult($"{_fieldName} can only contain letters, numbers, and spaces");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validation attribute for positive integers (IDs, counts, etc.)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PositiveIntegerAttribute : ValidationAttribute
    {
        private readonly string _fieldName;

        public PositiveIntegerAttribute(string fieldName = "Value")
        {
            _fieldName = fieldName;
            ErrorMessage = $"{{0}} must be a positive number";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!int.TryParse(value.ToString(), out int intValue))
                return new ValidationResult($"{_fieldName} must be a valid integer");

            if (intValue <= 0)
                return new ValidationResult($"{_fieldName} must be greater than 0");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validation attribute for Arabic text
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ArabicTextAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public ArabicTextAttribute(int minLength = 2, int maxLength = 300)
        {
            _minLength = minLength;
            _maxLength = maxLength;
            ErrorMessage = $"{{0}} must be valid Arabic text between {minLength} and {maxLength} characters";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var stringValue = value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue))
                return new ValidationResult("Arabic text cannot be empty");

            if (stringValue.Length < _minLength || stringValue.Length > _maxLength)
                return new ValidationResult($"Arabic text must be between {_minLength} and {_maxLength} characters");

            // Basic Arabic character validation (Unicode range for Arabic)
            bool hasArabic = stringValue.Any(c => (c >= '\u0600' && c <= '\u06FF'));
            if (!hasArabic && stringValue.Any(char.IsLetter))
                return new ValidationResult("Text must contain Arabic characters");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validation attribute for English text
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EnglishTextAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public EnglishTextAttribute(int minLength = 2, int maxLength = 300)
        {
            _minLength = minLength;
            _maxLength = maxLength;
            ErrorMessage = $"{{0}} must be valid English text between {minLength} and {maxLength} characters";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var stringValue = value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue))
                return new ValidationResult("English text cannot be empty");

            if (stringValue.Length < _minLength || stringValue.Length > _maxLength)
                return new ValidationResult($"English text must be between {_minLength} and {_maxLength} characters");

            // Check if contains only Latin characters
            bool hasNonLatin = stringValue.Any(c => (c >= '\u0600' && c <= '\u06FF'));
            if (hasNonLatin)
                return new ValidationResult("Text must contain only English characters");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validation attribute for descriptions (longer text)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DescriptionAttribute : ValidationAttribute
    {
        private readonly int _maxLength;

        public DescriptionAttribute(int maxLength = 2000)
        {
            _maxLength = maxLength;
            ErrorMessage = $"{{0}} cannot exceed {maxLength} characters";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            var stringValue = value.ToString();

            if (stringValue.Length > _maxLength)
                return new ValidationResult($"Description cannot exceed {_maxLength} characters");

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validation attribute for conditional required fields
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConditionalRequiredAttribute : ValidationAttribute
    {
        private readonly string _dependentProperty;
        private readonly object _dependentValue;

        public ConditionalRequiredAttribute(string dependentProperty, object dependentValue)
        {
            _dependentProperty = dependentProperty;
            _dependentValue = dependentValue;
            ErrorMessage = $"{{0}} is required when {dependentProperty} is {dependentValue}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dependentPropertyInfo = validationContext.ObjectType.GetProperty(_dependentProperty);
            if (dependentPropertyInfo == null)
                return new ValidationResult($"Property '{_dependentProperty}' not found");

            var dependentValue = dependentPropertyInfo.GetValue(validationContext.ObjectInstance);

            if ((dependentValue == null && _dependentValue == null) ||
                (dependentValue != null && dependentValue.Equals(_dependentValue)))
            {
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// Custom validation attribute for ID fields
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidIdAttribute : ValidationAttribute
    {
        private readonly bool _allowZero;

        public ValidIdAttribute(bool allowZero = false)
        {
            _allowZero = allowZero;
            ErrorMessage = _allowZero ? "{0} must be a valid non-negative number" : "{0} must be a positive number";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!int.TryParse(value.ToString(), out int intValue))
                return new ValidationResult($"ID must be a valid integer");

            if (!_allowZero && intValue <= 0)
                return new ValidationResult($"ID must be greater than 0");

            if (_allowZero && intValue < 0)
                return new ValidationResult($"ID cannot be negative");

            return ValidationResult.Success;
        }
    }
}
