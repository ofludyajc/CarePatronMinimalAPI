using api.Models;

namespace api.DAL.ClientDAL
{
    public interface IClientDAL
    {
        Task<Client[]> GetClients();

        Task CreateClient(Client client);

        Task<Client> UpdateClient(string ID, Client client);

        Task<List<Client>> SearchClients(string searchString);
    }
}
