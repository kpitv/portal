namespace Portal.Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmailAsync((string address, string subject, string message) email);
    }
}