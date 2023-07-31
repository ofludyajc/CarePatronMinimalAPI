namespace api.BLL.EmailBLL
{
    public interface IEmailBLL
    {
        Task Send(string emailAddress, string message);
    }
}
