using api.BLL.DocumentBLL;
using api.BLL.EmailBLL;
using api.DAL.ClientDAL;
using api.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace api.BLL.ClientBLL
{
    public class ClientBLL : IClientBLL
    {
        private readonly IClientDAL clientDAL;
        private readonly IEmailBLL  emailBLL;
        private readonly IDocumentBLL documentBLL;
        
        public ClientBLL(IClientDAL clientDAL, IEmailBLL emailBLL, IDocumentBLL documentBLL)
        {
            this.clientDAL = clientDAL;
            this.emailBLL = emailBLL;
            this.documentBLL = documentBLL;
        }

        public async Task CreateClient(Client client)
        {
            await clientDAL.CreateClient(client);
            await emailBLL.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
            await documentBLL.SyncDocumentsFromExternalSource(client.Email);
        }

        public async Task<Client[]> GetClients()
        {
            var clients = await clientDAL.GetClients();

            return clients;
        }

        public async Task UpdateClient(string ID, Client client)
        {
            var existingClient = await clientDAL.UpdateClient(ID, client);

            if (existingClient.Email != client.Email)
            {
                await emailBLL.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
                await documentBLL.SyncDocumentsFromExternalSource(client.Email);
            }
        }

        // Rody Dulfo 07/28/2023 - Create function for search clients
        public async Task<List<Client>> SearchClients(string searchString)
        {
            var searchClients = await clientDAL.SearchClients(searchString);

            return searchClients;
        }
    }
}
