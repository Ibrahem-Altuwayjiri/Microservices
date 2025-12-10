using Services.Email.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Domain.Entities
{
    //the entity has composite primary key
    public class TemplateDetails : ICreateEntity, IUpdateEntity
    {
        [ForeignKey("Template")]
        public int TemplateId { get; set; } //Primary key and foreign key
        public int VersionNumber { get; set; } = 0; // Primary key
        public Template Template { get; set; }
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateByClientIp { get; set; }
        public string? UpdateByUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateByClientIp { get; set; }
        public byte[]? HeaderImg { get; set; }
        public byte[]? SubHeaderImg { get; set; }
        public string? TitleColor { get; set; }
        public string? FirstLineColor { get; set; }
        public string? SecondLineColor { get; set; }
        public string? ThirdLineColor { get; set; }
        public string? FooterColor { get; set; }
        public byte[]? SubFooterImg { get; set; }
        public byte[]? FooterImg { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
