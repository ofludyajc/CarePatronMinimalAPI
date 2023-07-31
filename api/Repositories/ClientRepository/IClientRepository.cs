using api.Models;

namespace api.Repositories.ClientRepository
{
    public interface IClientRepository
    {
        Task<Client[]> GetClients();
        Task CreateClient(Client client);
        Task UpdateClient(string ID, Client client);
        Task<List<Client>> SearchClients(string searchString);
    }
}
