using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.DAL.ClientDAL
{
    public class ClientDAL : IClientDAL
    {
        private readonly DataContext dataContext;

        public ClientDAL(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task CreateClient(Client client)
        {
            await dataContext.AddAsync(client);
            await dataContext.SaveChangesAsync();
        }

        public async Task<Client[]> GetClients()
        {
            return await dataContext.Clients.ToArrayAsync();
        }

        public async Task<Client> UpdateClient(string ID, Client client)
        {
            var existingClient = await dataContext.Clients.FirstOrDefaultAsync(x => x.Id == ID);

            if (existingClient == null)
                throw new Exception("No existing client");

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Email = client.Email;
            existingClient.PhoneNumber = client.PhoneNumber;

            await dataContext.SaveChangesAsync();

            return existingClient;
        }

        public async Task<List<Client>> SearchClients(string searchString)
        {
            var searchedClients = new List<Client>();
            // Rody Dulfo 07/28/2023 -  Create separate getter for first name and last name
            var searchClientsByFirstName = await dataContext.Clients.Where(c => c.FirstName.ToLower().Contains(searchString.ToLower())).ToListAsync();
            var searchClientsByLastName = await dataContext.Clients.Where(c => c.LastName.ToLower().Contains(searchString.ToLower())).ToListAsync();

            // Rody Dulfo 07/28/2023 - Use union to avoid duplicates from first and second list
            searchedClients = searchClientsByFirstName.Union(searchClientsByLastName).ToList();

            return searchedClients;
        }

    }
}
