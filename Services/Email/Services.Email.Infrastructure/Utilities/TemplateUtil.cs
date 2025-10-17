using Services.Email.Domain.Entities;
using Services.Email.Infrastructure.Configuration.ExceptionHandlers;


namespace Services.Email.Infrastructure.Utilities
{
    public static class TemplateUtil
    {

        private readonly static string EmailTemplateLocationPath = Path.Combine(Directory.GetCurrentDirectory(), "Templates\\HTML\\email.html");

        private static string GetTemplateAsString(string templatePath)
        {
            using (StreamReader SourceReader = File.OpenText(templatePath))
            {
                return SourceReader.ReadToEnd();
            }

            throw new RestfulException("Internal Server Error", RestfulStatusCodes.InternalServerError);
        }

        public static string GetEmailTemplate(TemplateDetails templateDetails)
        {

            string htmlTemplate = GetTemplateAsString(EmailTemplateLocationPath);

            htmlTemplate = htmlTemplate.Replace("{HeaderImg}", templateDetails.HeaderImg.ToString());
            htmlTemplate = htmlTemplate.Replace("{SubHeaderImg}", templateDetails.SubHeaderImg.ToString());
            htmlTemplate = htmlTemplate.Replace("{TitleColor}", templateDetails.TitleColor);
            htmlTemplate = htmlTemplate.Replace("{FirstLineColor}", templateDetails.FirstLineColor);
            htmlTemplate = htmlTemplate.Replace("{SecondLineColor}", templateDetails.SecondLineColor);
            htmlTemplate = htmlTemplate.Replace("{ThirdLineColor}", templateDetails.ThirdLineColor);
            htmlTemplate = htmlTemplate.Replace("{FooterColor}", templateDetails.FooterColor);
            htmlTemplate = htmlTemplate.Replace("{SubFooterImg}", templateDetails.SubFooterImg.ToString());
            htmlTemplate = htmlTemplate.Replace("{FooterImg}", templateDetails.FooterImg.ToString());

            return htmlTemplate;
        }

        public static string SetValue(EmailContent emailContent, string htmlTemplate)
        {
            htmlTemplate = htmlTemplate.Replace("{Title}", emailContent.Title);
            htmlTemplate = htmlTemplate.Replace("{FirstLine}", emailContent.FirstLine);
            htmlTemplate = htmlTemplate.Replace("{SecondLine}", emailContent.SecondLine);
            htmlTemplate = htmlTemplate.Replace("{ThirdLine}", emailContent.ThirdLine);
            htmlTemplate = htmlTemplate.Replace("{Footer}", emailContent.Footer);

            return htmlTemplate;
        }
    }
}
