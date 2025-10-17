using Services.Email.Application.Models.Dto.Attachments;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.IService
{
    public interface IAttachmentsService
    {
        Task<IEnumerable<AttachmentsDto>> Create(List<CreateAttachmentsDto> createAttachments);
        Task<AttachmentsDto> GetByReferenceId(string ReferenceId);
        Task<AttachmentsDto> GetById(int Id);
        Task<IEnumerable<SendAttachmentsDto>> GetAttachments(int EmailDetailsId);
    }
}
