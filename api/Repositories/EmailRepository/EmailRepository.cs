using api.BLL.EmailBLL;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.EmailRepository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IEmailBLL emailBLL;

        public EmailRepository(IEmailBLL emailBLL) 
        {
            this.emailBLL = emailBLL;
        }    

        public async Task Send(string emailAddress, string message)
        {
            await emailBLL.Send(emailAddress, message);
        }
    }
}

