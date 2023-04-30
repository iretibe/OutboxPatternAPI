using OutboxPatternAPI.Models;

namespace OutboxPatternAPI.EmailOutboxService
{
    public interface IEmailOutboxService
    {
        Task<EmailOutbox> Add(EmailOutbox emailOutbox);
        Task<EmailOutbox> Update(EmailOutbox emailOutbox);
        IEnumerable<EmailOutbox> GetAll();
    }
}
