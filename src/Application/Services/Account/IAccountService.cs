
using Application.Dtos.User;

namespace Application.Services.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Checks for user by username or email. Sets user info and returns a valid token
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> GetTokenAsync(UserDto userDto, string password);

        /// <summary>
        /// Creates user and sets user info
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task RegisterAsync(UserDto userDto, string password);

        /// <summary>
        /// returns current user info by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserDto> GetUserAsync(string userId);
    }
}
