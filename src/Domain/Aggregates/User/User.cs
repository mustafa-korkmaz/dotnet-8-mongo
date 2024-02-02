namespace Domain.Aggregates.User
{
    public class User : Document
    {
        public string Username { get; private set; }

        public string Email { get; private set; }

        public string? NameSurname { get; private set; }

        public bool IsEmailConfirmed { get; private set; }

        public string? PasswordHash { get; private set; }

        private ICollection<string>? _claims;

        public IReadOnlyCollection<string>? Claims
        {
            get => _claims?.ToList();
            
            // mongo deserialization requires a setter
            private set => _claims = value == null ? new List<string>() : value.ToList();
        }

        public User(string id, string username, string email,bool isEmailConfirmed,
            DateTime createdAt) : base(id, createdAt)
        {
            Username = username;
            Email = email;
            IsEmailConfirmed = isEmailConfirmed;
        }

        public void AddClaim(string claim)
        {
            _claims ??= new List<string>();

            _claims.Add(claim);
        }

        public void SetNameSurname(string? nameSurname)
        {
            NameSurname = nameSurname;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}