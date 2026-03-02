using Services.Frontend.Web.Services.DTOs.LookupService;
using System.ComponentModel.DataAnnotations;

namespace Services.Frontend.Web.Services.DTOs.ManageServices
{
    public class HeaderValueDto
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public int HeaderId { get; set; }
        public HeaderDto? Header { get; set; }
    }
}
