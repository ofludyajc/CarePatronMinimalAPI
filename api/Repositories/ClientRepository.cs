using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public interface IClientRepository
    {
        Task<Client[]> GetClients();
        Task CreateClient(Client client);
        Task UpdateClient(string ID, Client client);
        Task<List<Client>> SearchClients(string searchString);
    }

    public class ClientRepository : IClientRepository
    {
        private readonly DataContext dataContext;
        private readonly IEmailRepository emailRepository;
        private readonly IDocumentRepository documentRepository;

        public ClientRepository(DataContext dataContext, IEmailRepository emailRepository, IDocumentRepository documentRepository)
        {
            this.dataContext = dataContext;
            this.emailRepository = emailRepository;
            this.documentRepository = documentRepository;
        }

        public async Task CreateClient(Client client)
        {
            await dataContext.AddAsync(client);
            await dataContext.SaveChangesAsync();

            await emailRepository.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
            await documentRepository.SyncDocumentsFromExternalSource(client.Email);
        }

        public Task<Client[]> GetClients()
        {
            return dataContext.Clients.ToArrayAsync();
        }

        public async Task UpdateClient(string ID, Client client)
        {
            var existingClient = await dataContext.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);

            if (existingClient == null)
                return;

            if (existingClient.Email != client.Email)
            {
                await emailRepository.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
                await documentRepository.SyncDocumentsFromExternalSource(client.Email);
            }

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Email = client.Email;
            existingClient.PhoneNumber = client.PhoneNumber;

            await dataContext.SaveChangesAsync();
        }

        // Rody Dulfo 07/28/2023 - Create function for search clients
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

