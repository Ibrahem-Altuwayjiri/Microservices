using Services.ServicesManagement.Application.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Models.Dto.Activities
{
    public class ActivitiesWithAuditDto : ICreateEntityDto, IUpdateEntityDto
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActive { get; set; }
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
    }
}
