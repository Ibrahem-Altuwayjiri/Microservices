using System.ComponentModel.DataAnnotations;

namespace Services.ServicesManagement.Application.Models.Dto.Domains
{
    public class CreateOrUpdateDomainsDto
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
    }
}
