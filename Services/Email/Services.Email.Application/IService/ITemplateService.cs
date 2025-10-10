using Services.Email.Application.Models.Dto.Template;


namespace Services.Email.Application.IService
{
    public interface ITemplateService
    {
        Task<TemplateDto> CreateTemplate(CreateTemplateDto createTemplate);
        Task<TemplateDto> UpdateTemplate(UpdateTemplateDto updateTemplate);

        Task<TemplateDto> GetTemplate(int id);
        Task<IEnumerable<TemplateDto>> GetAllTemplates();

    }
}
