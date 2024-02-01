
namespace Application.Dtos.User
{
    public class UserDto : DtoBase
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? NameSurname { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public ICollection<string>? Claims { get; set; }
    }
}
