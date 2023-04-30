namespace OutboxPatternAPI.Services
{
    public interface IMailService
    {
        bool Send(string sender, string subject, string body, bool isBodyHTML);
    }
}
