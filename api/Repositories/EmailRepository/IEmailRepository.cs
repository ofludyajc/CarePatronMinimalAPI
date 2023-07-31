namespace api.Repositories.EmailRepository
{
    public interface IEmailRepository
    {
        Task Send(string emailAddress, string message);
    }
}
