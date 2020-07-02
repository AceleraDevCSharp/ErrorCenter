using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using ErrorCenter.Services.Models;
using ErrorCenter.Persistence.EF.Repositories;

namespace ErrorCenter.Services {
  public class AuthenticateUserService {
    private IUsersRepository _repository;
    private readonly IConfiguration _config;

    public AuthenticateUserService(
      IUsersRepository repository,
      IConfiguration config
    ) {
      _repository = repository;
      _config = config;
    }

    public Session execute(string email, string password) {
      var user = _repository.FindByEmail(email);

      if (user == null) return null;

      var tokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.ASCII.GetBytes(_config["JWTSecret"]);

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new Claim[] {
          new Claim(ClaimTypes.Email, user.Email.ToString()),
          new Claim(ClaimTypes.Role, user.Environment.ToString()),
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