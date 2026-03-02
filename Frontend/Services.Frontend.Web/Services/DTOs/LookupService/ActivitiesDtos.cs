using System.ComponentModel.DataAnnotations;
using Services.Frontend.Web.Validations;

namespace Services.Frontend.Web.Services.DTOs.LookupService
{
    public class ActivitiesDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateOrUpdateActivitiesDto
    {
        [ValidId(allowZero: true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Arabic name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Arabic name must be between 2 and 200 characters")]
        [ArabicText(minLength: 2, maxLength: 200)]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "English name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "English name must be between 2 and 200 characters")]
        [EnglishText(minLength: 2, maxLength: 200)]
        public string NameEn { get; set; }
    }
}
