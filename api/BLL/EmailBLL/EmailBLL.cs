namespace api.BLL.EmailBLL
{
    public class EmailBLL : IEmailBLL
    {
        public async Task Send(string emailAddress, string message)
        {
            // this simulates sending an email
            // leave this delay as 3s to emulate real life
            await Task.Delay(3000);
        }
    }
}
