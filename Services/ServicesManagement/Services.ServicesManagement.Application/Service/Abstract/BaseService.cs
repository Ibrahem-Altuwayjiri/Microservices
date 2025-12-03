using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.ServicesManagement.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesManagement.Application.Service.Abstract
{
    public class BaseService
    {
        protected internal IMapper _mapper;
        protected internal IUnitOfWork _unitOfWork;
        protected internal IHttpContextAccessor _httpContextAccessor;

        public BaseService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;

            
        }
    }

    
}
