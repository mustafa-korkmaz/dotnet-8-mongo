namespace Domain.Aggregates.User
{
    public interface IUserRepository : IRepository<User>
    {
        Task<ListDocumentResponse<User>> SearchAsync(int offset, int limit, string? searchText);

        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByUsernameAsync(string username);
    }
}
