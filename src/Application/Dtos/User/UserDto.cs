namespace Application.Dtos.User
{
    public record UserDto(string Username, string Email, string? NameSurname, bool IsEmailConfirmed,
        ICollection<string>? Claims, DateTime CreatedAt) : DtoBase(CreatedAt);
}