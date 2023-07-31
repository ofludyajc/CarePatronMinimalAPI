using api.Models;

namespace api.BLL.ClientBLL
{
    public interface IClientBLL
    {
        Task<Client[]> GetClients();

        Task CreateClient(Client client);

        Task UpdateClient(string ID, Client client);

        Task<List<Client>> SearchClients(string searchString);
    }
}
