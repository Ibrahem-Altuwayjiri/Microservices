using Services.Email.Application.Models.Dto.EmailDetails;
using Services.Email.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.IService
{
    public interface IEmailService
    {
        Task<EmailDetailsDto> CreateEmail(CreateEmailDetailsDto emailDetails);
        Task<IEnumerable<EmailDetails>> GetUnSentEmails();
        Task<bool> IsHasUnSentEmail();
        Task Send();
        
    }
}
