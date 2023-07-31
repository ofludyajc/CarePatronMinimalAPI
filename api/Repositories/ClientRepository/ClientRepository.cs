using api.BLL.ClientBLL;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.ClientRepository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IClientBLL clientBLL;

        public ClientRepository(IClientBLL clientBLL)
        {
            this.clientBLL = clientBLL;
        }

        public async Task CreateClient(Client client)
        {
           await clientBLL.CreateClient(client);
        }

        public async Task<Client[]> GetClients()
        {
            var clients = await clientBLL.GetClients();

            return clients;
        }

        public async Task UpdateClient(string ID, Client client)
        {
            await clientBLL.UpdateClient(ID, client);
        }

        // Rody Dulfo 07/28/2023 - Create function for search clients
        public async Task<List<Client>> SearchClients(string searchString)
        {
            var searchedClients = await clientBLL.SearchClients(searchString);

            return searchedClients;
        }
    }
}

