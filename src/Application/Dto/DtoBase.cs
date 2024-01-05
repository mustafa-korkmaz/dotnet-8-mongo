namespace Application.Dto
{
    public abstract class DtoBase
    {
        public string Id { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

    public class ListDtoResponse<TDto> where TDto : DtoBase
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDto> Items { get; set; } = new List<TDto>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long RecordsTotal { get; set; }
    }

    public class ListDtoRequest
    {
        public bool IncludeRecordsTotal { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}