using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.Services
{
    public class AuthenticateUserService : IAuthenticateUserService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IConfiguration config;

        public AuthenticateUserService(
          IUsersRepository usersRepository,
          IPasswordHasher<User> passwordHasher,
          IConfiguration config
        )
        {
            this.usersRepository = usersRepository;
            this.passwordHasher = passwordHasher;
            this.config = config;
        }

        public async Task<SessionResponseDTO> Authenticate(SessionRequestDTO data)
        {
            var user = await usersRepository.FindByEmail(data.Email);

            if (user == null)
            {
                throw new AuthenticationException(
                  "Invalid e-mail/password combination",
                  StatusCodes.Status401Unauthorized
                );
            }

            var valid = passwordHasher
              .VerifyHashedPassword(user, user.PasswordHash, data.Password);

            if (valid != PasswordVerificationResult.Success)
            {
                throw new AuthenticationException(
                  "Invalid e-mail/password combination",
                  StatusCodes.Status401Unauthorized
                );
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(config["JWTSecret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
              Subject = new ClaimsIdentity(
                new Claim[] {
                  new Claim(ClaimTypes.Email, user.Email),
                }
              ),
              Expires = DateTime.UtcNow.AddDays(1),
              SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
              )
            };

            var roles = await usersRepository.GetUserRoles(user);
            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(
                  new Claim(ClaimTypes.Role, role)
                );
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new SessionResponseDTO()
            {
                Email = user.Email,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}