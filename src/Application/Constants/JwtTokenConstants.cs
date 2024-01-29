using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.Constants
{
    public class JwtTokenConstants
    {
        public static SecurityKey IssuerSigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes("!_*a_quite_long_pass_phrase_goes_here*_!"));

        public static SigningCredentials SigningCredentials => new(IssuerSigningKey, SecurityAlgorithms.HmacSha256);

        public static TimeSpan TokenExpirationTime => TimeSpan.FromDays(1);

        public static string Issuer => "mustafakorkmaz";

        public static string Audience => "Audience";
    }
}
