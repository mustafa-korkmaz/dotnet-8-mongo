using Application.Constants;
using Application.Exceptions;
using AutoMapper;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Dtos.User;
using Domain.Aggregates.User;
using Infrastructure.Repositories;

namespace Application.Services.Account
{
    public class AccountService : ServiceBase<UserRepository, User, UserDto>, IAccountService
    {
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork uow, ILogger<AccountService> logger, IMapper mapper)
            : base(uow, logger, mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> GetTokenAsync(UserDto userDto, string password)
        {
            User? user;

            if (userDto.Username.Contains("@")) // login via email
            {
                user = await Repository.GetByEmailAsync(userDto.Email);
            }
            else // login via username
            {
                user = await Repository.GetByUsernameAsync(userDto.Username);
            }

            if (user == null)
            {
                throw new ValidationException(ErrorMessages.UserNotFound);
            }

            var isPasswordValid = VerifyHashedPassword(user.PasswordHash!, password);

            if (!isPasswordValid)
            {
                throw new ValidationException(ErrorMessages.IncorrectUsernameOrPassword);
            }

            userDto = _mapper.Map<UserDto>(user);

            return GenerateToken(userDto);
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            var user = await Repository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new ValidationException(ErrorMessages.UserNotFound);
            }

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task RegisterAsync(UserDto userDto, string password)
        {
            var userByName = await Repository.GetByUsernameAsync(userDto.Username.Normalize());

            if (userByName != null)
            {
                throw new ValidationException(ErrorMessages.UserExists);
            }

            var userByEmail = await Repository.GetByEmailAsync(userDto.Email.Normalize());

            if (userByEmail != null)
            {
                throw new ValidationException(ErrorMessages.UserExists);
            }

            var user = _mapper.Map<User>(userDto);

            user.SetPasswordHash(HashPassword(password));

            await Repository.InsertOneAsync(user);
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;

            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return AreHashesEqual(buffer3, buffer4);
        }

        private string GenerateToken(UserDto user)
        {
            var now = DateTime.UtcNow;

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id!),
                new(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(now).ToString(), ClaimValueTypes.Integer64),
                new(JwtRegisteredClaimNames.Email, user.Email)
            };

            if (user.Claims != null)
                foreach (var userClaim in user.Claims.Distinct())
                {
                    jwtClaims.Add(new Claim("perms", userClaim));
                }

            var jwt = new JwtSecurityToken(
                issuer: JwtTokenConstants.Issuer,
                audience: JwtTokenConstants.Audience,
                claims: jwtClaims,
                notBefore: now,
                expires: DateTime.UtcNow.Add(JwtTokenConstants.TokenExpirationTime),
                signingCredentials: JwtTokenConstants.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
          
            for (int i = 0; i < minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }
    }
}
