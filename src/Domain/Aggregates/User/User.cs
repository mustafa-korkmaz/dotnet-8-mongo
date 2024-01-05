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
            private set => //todo : check if this is needed
                _claims = value == null ? new List<string>() : value.ToList();
        }

        public User(string id, string username, string email, string? nameSurname, bool isEmailConfirmed, DateTime createdAt) : base(id)
        {
            Username = username;
            Email = email;
            NameSurname = nameSurname;
            IsEmailConfirmed = isEmailConfirmed;
            CreatedAt = createdAt;
        }

        public void AddClaim(string claim)
        {
            if (_claims == null)
            {
                _claims = new List<string>();
            }

            _claims.Add(claim);
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}