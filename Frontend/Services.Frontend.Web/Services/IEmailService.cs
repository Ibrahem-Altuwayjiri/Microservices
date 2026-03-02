namespace Services.Frontend.Web.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(SendEmailRequest request);
        Task<List<TemplateDto>> GetTemplatesAsync();
        Task<TemplateDto> GetTemplateByIdAsync(int id);
    }

    public class SendEmailRequest
    {
        public int TemplateId { get; set; }
        public List<string> Recipients { get; set; }
        public Dictionary<string, string> Variables { get; set; }
    }

    public class TemplateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
    }
}
