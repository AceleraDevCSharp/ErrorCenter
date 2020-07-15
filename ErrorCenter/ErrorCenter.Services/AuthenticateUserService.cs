using System;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using ErrorCenter.Services.Interfaces;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Repositories;
using ErrorCenter.Services.Providers.HashProvider.Models;

namespace ErrorCenter.Services {
  public class AuthenticateUserService : IAuthenticateUserService {
    private IUsersRepository _repository;
    private IHashProvider _hash;
    private readonly IConfiguration _config;

    public AuthenticateUserService(
      IUsersRepository repository,
      IHashProvider hash,
      IConfiguration config
    ) {
      _repository = repository;
      _hash = hash;
      _config = config;
    }

    public async Task<Session> Execute(string email, string password) {
      var user = await _repository.FindByEmail(email);

      if (user == null) return null;

      if (!_hash.VerifyHash(password, user.Password)) return null;

      var tokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.ASCII.GetBytes(_config["JWTSecret"]);

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new Claim[] {
          new Claim(ClaimTypes.Email, user.Email),
          new Claim(ClaimTypes.Role, user.Environment),
        }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return new Session(
        user.Email,
        user.Environment,
        tokenHandler.WriteToken(token)
      );
    }
  }
}