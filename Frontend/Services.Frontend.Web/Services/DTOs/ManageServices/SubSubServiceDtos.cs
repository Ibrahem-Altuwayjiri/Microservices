using System.ComponentModel.DataAnnotations;
using Services.Frontend.Web.Validations;

namespace Services.Frontend.Web.Services.DTOs.ManageServices
{
    public class SubSubServiceDto
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public string SubServiceId { get; set; }
        public SubServiceDto SubService { get; set; }
    }



    public class CreateOrUpdateSubSubServiceDto
    {
        [Required(ErrorMessage = "Service ID is required")]
        [ValidId(allowZero: false)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Service name must be between 2 and 200 characters")]
        [ValidName(minLength: 2, maxLength: 200, fieldName: "Service Name", allowNumbers: true, allowSpecialChars: false)]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "Service name is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Service name must be between 2 and 200 characters")]
        [ValidName(minLength: 2, maxLength: 200, fieldName: "Service Name", allowNumbers: true, allowSpecialChars: false)]
        public string NameEn { get; set; }

        [Required(ErrorMessage = "Sub Service ID is required")]
        [ValidId(allowZero: false)]
        public string SubServiceId { get; set; }
    }
}
