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

namespace ErrorCenter.Services.Services {
  public class AuthenticateUserService : IAuthenticateUserService {
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IConfiguration config;

    public AuthenticateUserService(
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      IConfiguration config
    ) {
      this.userManager = userManager;
      this.signInManager = signInManager;
      this.config = config;
    }

    public async Task<Session> Authenticate(string email, string password) {
      var user = await userManager.FindByEmailAsync(email);

      if (user == null) {
        throw new AuthenticationException(
          "Invalid e-mail/password combination",
          StatusCodes.Status401Unauthorized
        );
      }

      var valid = await signInManager
        .PasswordSignInAsync(user.UserName, password, false, true);

      if (!valid.Succeeded) {
        throw new AuthenticationException(
          "Invalid e-mail/password combination",
          StatusCodes.Status401Unauthorized
        );
      }

      var tokenHandler = new JwtSecurityTokenHandler();

      var key = Encoding.ASCII.GetBytes(config["JWTSecret"]);

      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(
          new Claim[] {
            new Claim(ClaimTypes.Email, user.Email),
          }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(
          new SymmetricSecurityKey(key),
          SecurityAlgorithms.HmacSha256Signature
        )
      };

      var roles = await userManager.GetRolesAsync(user);
      foreach (var role in roles) {
        tokenDescriptor.Subject.AddClaim(
          new Claim(ClaimTypes.Role, role)
        );
      }

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return new Session(
        user.Email,
        tokenHandler.WriteToken(token)
      );
    }
  }
}