namespace Application.Dtos
{
    public abstract record DtoBase
    {
        public string? Id { get; set; }
        
        public DateTime CreatedAt { get; init; }
    }

    public readonly record struct ListDtoResponse<TDto>(IReadOnlyCollection<TDto> Items, long RecordsTotal)
        where TDto : DtoBase;

    public readonly record struct ListDtoRequest(bool IncludeRecordsTotal, int Offset, int Limit);
}