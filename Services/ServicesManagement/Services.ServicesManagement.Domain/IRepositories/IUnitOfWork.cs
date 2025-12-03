using Services.ServicesManagement.Domain.Entities.Lookups;
using Services.ServicesManagement.Domain.Entities.ServiceInfo;
using Services.ServicesManagement.Domain.Entities.ServiceStructure;

namespace Services.ServicesManagement.Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        //IMediaFileRepository MediaFileRepository { get; set; }
        IGenericRepository<ServiceDetails> ServiceDetailsRepository { get; set; }

        IGenericRepository<Activities> ActivitiesRepository { get; set; }
        IGenericRepository<DocumentName> DocumentNameRepository { get; set; }
        IGenericRepository<DocumentValue> DocumentValueRepository { get; set; }
        IGenericRepository<Domains> DomainsRepository { get; set; }
        IGenericRepository<Header> HeaderRepository { get; set; }
        IGenericRepository<HeaderValue> HeaderValueRepository { get; set; }
        IGenericRepository<MainService> MainServiceRepository { get; set; }
        IGenericRepository<ServiceActivities> ServiceActivitiesRepository { get; set; }
        IGenericRepository<ServiceDomains> ServiceDomainsRepository { get; set; }
        IGenericRepository<ServiceTags> ServiceTagsRepository { get; set; }
        IGenericRepository<SubService> SubServiceRepository { get; set; }
        IGenericRepository<SubSubService> SubSubServiceRepository { get; set; }
        IGenericRepository<Tags> TagsRepository { get; set; }




        Task<int> CompletedAsync();
    }
}
