namespace Application.Dtos
{
    public abstract record DtoBase(DateTime CreatedAt)
    {
        public string? Id { get; set; }
    }

    public readonly record struct ListDtoResponse<TDto>(IReadOnlyCollection<TDto> Items, long RecordsTotal)
        where TDto : DtoBase;

    public readonly record struct ListDtoRequest(bool IncludeRecordsTotal, int Offset, int Limit);
}