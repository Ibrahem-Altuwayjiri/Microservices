using System.ComponentModel.DataAnnotations;

namespace Services.Frontend.Web.Services.DTOs.ManageServices
{
    public class DocumentValueDto
    {
        public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        [Required]
        public string NameEn { get; set; }
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public int DocumentNameId { get; set; }

    }
}
