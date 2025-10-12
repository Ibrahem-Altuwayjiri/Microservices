using AutoMapper;
using Microsoft.AspNetCore.Http;
using Services.Email.Application.IService;
using Services.Email.Application.Models.Dto.EmailDetails;
using Services.Email.Domain.Entities;
using Services.Email.Domain.IRepositories;
using Services.Email.Infrastructure.Configuration.ExceptionHandlers;
using Services.Email.Infrastructure.Helper;
using Services.Email.Infrastructure.Utilities;
using Services.Email.Infrastructure.Utilities.EmailUtil;
using Services.Email.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Email.Application.Service
{
    public class EmailService : IEmailService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITemplateService _templateService;
        private readonly IAttachmentsService _attachmentsService;


        public EmailService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ITemplateService templateService, IAttachmentsService attachmentsService)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _templateService = templateService;
            _attachmentsService = attachmentsService;
        }
        public async Task<EmailDetailsDto> CreateEmail(CreateEmailDetailsDto emailDetails)
        {
           var entity = _mapper.Map<EmailDetails>(emailDetails);

            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim?.Subject?.Claims.FirstOrDefault(u => u.Properties.Values.Any(x => x.Equals("sub")))?.Value;


            entity.SenderInfo = new SenderInfo
            {
                UserId = userId,
                ClientIp = IpHelper.GetClientIp(_httpContextAccessor.HttpContext)
            };

            await _unitOfWork.EmailDetailsRepository.Add(entity);
            await _unitOfWork.CompletedAsync();

            return _mapper.Map<EmailDetailsDto>(entity);
        }

        public Task<IEnumerable<EmailDetails>> GetUnSentEmails()
        {
            var emails = _unitOfWork.EmailDetailsRepository.FindWithInclude(m => m.IsSend == false && m.ScheduleDate <= DateTime.Now.ToLocalTime()
                                                                            , e => e.SenderInfo
                                                                            , e => e.EmailRecipients
                                                                            , e => e.EmailContent
                                                                            , e => e.Attachments
                                                                            , e => e.Template);
            return emails;
        }

        public async Task<bool> IsHasUnSentEmail()
        {
            return await _unitOfWork.EmailDetailsRepository.IsExists(m => m.IsSend == false);
        }

        public async Task Send()
        {
            var emails = await GetUnSentEmails();
            if (emails == null)
                return;

            foreach (var email in emails)
            {
                await SendAsync(email);
            }

        }

        private async Task SendAsync(EmailDetails email)
        {
            try
            {

                    var template = await _templateService.GetTemplate(email.TemplateId);
                    var emailTemplate = TemplateUtil.GetEmailTemplate(_mapper.Map<TemplateDetails>(template.templateDetails)); // get HTML template after adding base email body (header img , footer img, text color)
                    var emailContent = TemplateUtil.SetValue(email.EmailContent, emailTemplate); // adding Email content

                    var attachments = await _attachmentsService.GetAttachments(email.Id);

                    await EmailUtil.SendEmail(new SendEmailDetails
                    {
                        Subject = email.EmailContent.Subject,
                        ToEmails = email.EmailRecipients.Where(m => m.RecipientTypeId == (int)RecipientTypeEnum.To).Select(e => e.Email).ToArray(),
                        CcEmails = email.EmailRecipients.Where(m => m.RecipientTypeId == (int)RecipientTypeEnum.CC).Select(e => e.Email).ToArray(),
                        BccEmails = email.EmailRecipients.Where(m => m.RecipientTypeId == (int)RecipientTypeEnum.BCC).Select(e => e.Email).ToArray(),
                        Attachments = attachments.Select(m => new EmailAttachment { FileName = m.FileName, FileBytes = m.FileBytes }).ToList(),
                        Content = emailContent
                    });
                    email.IsSend = true;
                    email.SendDate = DateTime.Now.ToLocalTime();
                    await _unitOfWork.EmailDetailsRepository.Update(email);
                    await _unitOfWork.CompletedAsync();
                

            }
            catch (Exception e)
            {
                var currentEmail = await _unitOfWork.EmailDetailsRepository.FindOneOrDefault(m => m.Id == email.Id);
                if (currentEmail != null)
                {
                    currentEmail.TryNum += 1;
                    currentEmail.LastTrySend = DateTime.UtcNow.ToLocalTime();
                    await _unitOfWork.EmailDetailsRepository.Update(currentEmail);
                }
                await _unitOfWork.EmailErrorLogRepository.Add(new EmailErrorLog
                {
                    Date = DateTime.UtcNow.ToLocalTime(),
                    EmailDetailsId = email.Id,
                    Message = e.Message
                });

                await _unitOfWork.CompletedAsync();


            }
        }
    }
}
