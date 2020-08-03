using System;
using System.Threading.Tasks;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Errors;
using Microsoft.AspNetCore.Identity;

namespace ErrorCenter.Services.Services {
  public class UsersService : IUsersService {
    private readonly IUsersRepository usersRepository;
    private readonly IEnvironmentsRepository environmentsRepository;
    private readonly IPasswordHasher<User> passwordHasher;

    public UsersService(
      IUsersRepository usersRepository,
      IEnvironmentsRepository environmentsRepository,
      IPasswordHasher<User> passwordHasher
    ) {
      this.usersRepository = usersRepository;
      this.environmentsRepository = environmentsRepository;
      this.passwordHasher = passwordHasher;
    }

    public async Task<User> CreateNewUser(UserDTO newUser) {
      var userEmail = await usersRepository.FindByEmail(newUser.Email);

      if (userEmail != null)
        throw new UserException("E-mail is already being used", 400);

      var environmentName = await environmentsRepository.FindByName(newUser.Environment);

      if (environmentName == null)
        throw new EnvironmentException("Environment not found", 404);

      var user = new User() {
        Email = newUser.Email,
        UserName = newUser.Email,
        EmailConfirmed = true,
        CreatedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
      };

      user.PasswordHash = passwordHasher.HashPassword(user, newUser.Password);

      user = await usersRepository.Create(user, newUser.Environment);

      return user;
    }
  }
}
