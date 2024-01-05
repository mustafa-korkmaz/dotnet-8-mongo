
namespace Presentation.ViewModels.Identity
{
    public class UserViewModel : ViewModelBase
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? NameSurname { get; set; }

        public IEnumerable<string> Claims { get; set; }
    }
}
