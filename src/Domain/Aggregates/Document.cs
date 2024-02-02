namespace Domain.Aggregates
{
    public abstract class Document : IDocument
    {
        public string Id { get; }
        public DateTime CreatedAt { get; }

        protected Document(string id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }
    }

    public interface IDocument
    {
        string Id { get; }

        DateTime CreatedAt { get; }
    }

    public class ListDocumentRequest
    {
        public bool IncludeRecordsTotal { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }

    public class ListDocumentResponse<TDocument> where TDocument : IDocument
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDocument> Items { get; set; } = null!;

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long RecordsTotal { get; set; }
    }
}