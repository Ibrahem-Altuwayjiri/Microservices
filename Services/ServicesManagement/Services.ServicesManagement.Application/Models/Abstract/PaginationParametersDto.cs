using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Models.Abstract
{
    public class PaginationParametersDto
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
