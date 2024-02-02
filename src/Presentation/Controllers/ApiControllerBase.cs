using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// returns user id from user principal claims.
        /// If not exists throws error
        /// </summary>
        /// <returns></returns>
        protected string GetUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier);

            var userId = userIdClaim?.Value;

            if (userId == null)
            {
                throw new NullReferenceException("UserId cannot be null");
            }

            return userId;
        }
    }
}