namespace Domain.Aggregates
{
    public interface IRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument?> GetByIdAsync(string id);

        /// <summary>
        /// Base listing interface without any filters
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ListDocumentResponse<TDocument>> ListAsync(ListDocumentRequest request);
        Task InsertOneAsync(TDocument document);
        Task InsertManyAsync(ICollection<TDocument> documents);
        Task ReplaceOneAsync(TDocument document);
        Task DeleteByIdAsync(string id);
    }
}