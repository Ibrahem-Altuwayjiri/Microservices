

namespace Services.Email.Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IAttachmentsRepository AttachmentsRepository { get; set; }
        IEmailContentRepository EmailContentRepository { get; set; }
        IEmailDetailsRepository EmailDetailsRepository { get; set; }
        IEmailErrorLogRepository EmailErrorLogRepository { get; set; }
        IEmailRecipientRepository EmailRecipientRepository { get; set; }
        IRecipientTypeRepository RecipientTypeRepository { get; set; }
        ISenderInfoRepository SenderInfoRepository { get; set; }
        ITemplateDetailsRepository TemplateDetailsRepository { get; set; }
        ITemplateRepository TemplateRepository { get; set; }

        Task<int> CompletedAsync();
    }
}
