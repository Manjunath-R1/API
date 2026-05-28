namespace ThoughtFocus.Business.Interfaces
{
    using ThoughtFocus.Domain.CustomView;


    public interface IEmailTemplateHeaderFooter
    {
        EmailTemplateUploadResponse SaveEmailTemplateFooter(string emailTemplateFooter,long userId);

    }
}
