using Services.Frontend.Web.Services.DTOs.LookupService;
using Services.Frontend.Web.Validations;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace Services.Frontend.Web.Services.DTOs.ManageServices
{
    public class ServiceDetailsDto
    {
        public string Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string? URL { get; set; }
        public bool IsActive { get; set; } = true;
        public string MainServiceId { get; set; }
        public MainServiceDto MainService { get; set; }
        public string SubServiceId { get; set; }
        public SubServiceDto SubService { get; set; }
        public string SubSubServiceId { get; set; }
        public SubSubServiceDto SubSubService { get; set; }

        // Related 
        public List<ActivitiesDto> Activities { get; set; }
        public List<TagsDto> Tags { get; set; }
        public List<DomainsDto> Domains { get; set; }
        //public List<DocumentValueDto> DocumentValue { get; set; }
        public List<HeaderValueDto> HeaderValue { get; set; }
        public List<StepsDto> Steps { get; set; }
        public List<PrerequisitesDto> Prerequisites { get; set; }
        public List<RequiredDocumentsDto> RequiredDocuments { get; set; }
    }



    public class CreateOrUpdateServiceDetailsDto
    {
        // Null for new items (server generates GUID); populated for updates
        public string? Id { get; set; }

        [Required(ErrorMessage = "Arabic name is required")]
        [StringLength(300, MinimumLength = 2, ErrorMessage = "Arabic name must be between 2 and 300 characters")]
        [ArabicText(minLength: 2, maxLength: 300)]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "English name is required")]
        [StringLength(300, MinimumLength = 2, ErrorMessage = "English name must be between 2 and 300 characters")]
        [EnglishText(minLength: 2, maxLength: 300)]
        public string NameEn { get; set; }

        [Description(maxLength: 2000)]
        public string DescriptionAr { get; set; }

        [Description(maxLength: 2000)]
        public string DescriptionEn { get; set; }

        public string? URL { get; set; }

        public List<int>? Activities { get; set; }
        public List<int>? Tags { get; set; }
        public List<int>? Domains { get; set; }
        public List<DocumentValueDto>? DocumentsValue { get; set; }
        public List<HeaderValueDto>? HeadersValue { get; set; }
        public List<StepsDto>? StepsValue { get; set; }
        public List<PrerequisitesDto>? PrerequisitesValue { get; set; }
        public List<RequiredDocumentsDto>? RequiredDocumentsValue { get; set; }

        // Service structure references
        public string? MainServiceId { get; set; }
        public string? SubServiceId { get; set; }
        public string? SubSubServiceId { get; set; }


        //for view only
        public List<ActivitiesDto>? AllActivities { get; set; }
        public List<DomainsDto>? AllDomains { get; set; }
        public List<TagsDto>? AllTags { get; set; }
        public List<HeaderDto>? AllHeaders { get; set; }
        //public List<DocumentDto>? AllDocuments { get; set; }

        // Service structure lists for dropdowns
        public List<MainServiceDto>? AllMainServices { get; set; }
        public List<SubServiceDto>? AllSubServices { get; set; }
        public List<SubSubServiceDto>? AllSubSubServices { get; set; }

        public static CreateOrUpdateServiceDetailsDto FromServiceDetailsDto(ServiceDetailsDto details)
        {
            return new CreateOrUpdateServiceDetailsDto
            {
                Id = details.Id,
                NameAr = details.NameAr,
                NameEn = details.NameEn,
                DescriptionAr = details.DescriptionAr,
                DescriptionEn = details.DescriptionEn,
                URL = details.URL,
                MainServiceId = details.MainServiceId,
                SubServiceId = details.SubServiceId,
                SubSubServiceId = details.SubSubServiceId,
                Activities = details.Activities?.Select(a => a.Id).ToList(),
                Tags = details.Tags?.Select(t => t.Id).ToList(),
                Domains = details.Domains?.Select(d => d.Id).ToList(),
                HeadersValue = details.HeaderValue,
                StepsValue = details.Steps,
                PrerequisitesValue = details.Prerequisites,
                RequiredDocumentsValue = details.RequiredDocuments
            };
        }

    }
}
