namespace api.Repositories.DocumentRepository
{
    public interface IDocumentRepository
    {
        Task SyncDocumentsFromExternalSource(string email);
    }
}
