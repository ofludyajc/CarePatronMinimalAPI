namespace api.BLL.DocumentBLL
{
    public class DocumentBLL : IDocumentBLL
    {
        public async Task SyncDocumentsFromExternalSource(string _)
        {
            // this simulates sending an email
            // leave this delay as 3s to emulate real life
            await Task.Delay(3000);
        }
    }
}
