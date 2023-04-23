namespace EmailService
{
    public interface IEmailSenderStrategy
    {
        void SendEmailAsync(Message message);
    }
}
