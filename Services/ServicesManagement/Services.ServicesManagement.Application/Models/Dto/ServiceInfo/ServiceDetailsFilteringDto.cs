using Services.ServicesManagement.Application.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Models.Dto.ServiceInfo
{
    public class ServiceDetailsFilteringDto
    {
        public string? MainServiceId { get; set; } = string.Empty;
        public string? SubServiceId { get; set; } = string.Empty;
        public string? SubSubServiceId { get; set; } = string.Empty;
        public List<int>? ActivityIds { get; set; } = null;
        public List<int>? DomainIds { get; set; } = null;
        public List<int>? TagIds { get; set; } = null;
        public string? filter { get; set; } = string.Empty;
        public PaginationParametersDto? pagination { get; set; } = null;
    }
}
