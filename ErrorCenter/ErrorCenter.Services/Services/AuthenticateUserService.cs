using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.IRepository;

namespace ErrorCenter.Services.Services {
  public class AuthenticateUserService : IAuthenticateUserService {
    private IUsersRepository _repository;
    private UserManager<User> _userManager;
    private PasswordValidator<User> _passwordValidator;
    private readonly IConfiguration _config;

    public AuthenticateUserService(
      IUsersRepository repository,
      UserManager<User> userManager,
      PasswordValidator<User> passwordValidator,
      IConfiguration config
    ) {
      _repository = repository;
      _userManager = userManager;
      _passwordValidator = passwordValidator;
      _config = config;
    }

    public async Task<Session> Authenticate(string email, string password) {
      var user = await _repository.FindByEmail(email);

      if (user == null) {
        throw new AuthenticationException(
          "Invalid e-mail/password combination",
          StatusCodes.Status401Unauthorized
        );
      }

      var valid = await _passwordValidator.ValidateAsync(_userManager, user, password);

      if (!valid.Succeeded) {
        throw new AuthenticationException(
          "Invalid e-mail/password combination",
          StatusCodes.Status401Unauthorized
        );
      }

      var tokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.ASCII.GetBytes(_config["JWTSecret"]);

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new Claim[] {
                          new Claim(ClaimTypes.Email, user.Email),
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
        null, // Role do usuário
        tokenHandler.WriteToken(token)
      );
    }
  }
}