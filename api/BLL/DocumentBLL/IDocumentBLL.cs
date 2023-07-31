namespace api.BLL.DocumentBLL
{
    public interface IDocumentBLL
    {
        Task SyncDocumentsFromExternalSource(string email);
    }
}
