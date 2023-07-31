using api.BLL.DocumentBLL;
using System;

namespace api.Repositories.DocumentRepository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly IDocumentBLL documentBLL;

        public DocumentRepository(IDocumentBLL documentBLL)
        {
            this.documentBLL = documentBLL;
        }

        public async Task SyncDocumentsFromExternalSource(string _)
        {
            await documentBLL.SyncDocumentsFromExternalSource(_);
        }
    }
}

