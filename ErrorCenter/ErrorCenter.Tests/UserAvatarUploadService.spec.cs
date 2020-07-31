using System;
using System.IO;

using Xunit;
using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Services.Fakes;
using ErrorCenter.Services.Providers.StorageProvider.Fakes;
using ErrorCenter.Services.Providers.StorageProvider.Model;

namespace ErrorCenter.Tests.Services {
  public class UserAvatarUploadServiceTest {
    private readonly IUsersRepository usersRepository;
    private readonly IStorageProvider storageProvider;
    private readonly IUserAvatarUploadService userAvatarUploadService;

    public UserAvatarUploadServiceTest() {
      usersRepository = new FakeUsersRepository();
      storageProvider = new FakeStorageProvider();
      userAvatarUploadService = new UserAvatarUploadService(
        usersRepository,
        storageProvider
      );
    }

    [Fact]
    public async void Should_Be_Able_To_Upload_Users_Avatar() {
      // Arrange
      var user = new User() {
        Email = "johndoe@example.com",
        UserName = "johndoe@example.com",
        EmailConfirmed = true,
        PasswordHash = "password-123",
        Avatar = "default.png"
      };
      await usersRepository.Create(user, "some-role");

      FormFile file;
      using (var stream = File.OpenRead("avatar-placeholder.png")) {
        file = new FormFile(
          stream,
          0,
          stream.Length,
          null,
          Path.GetFileName(stream.Name)
        ) {
          Headers = new HeaderDictionary(),
          ContentType = "image/png"
        };
      };

      var avatar = new UserAvatarDTO(file);

      // Act
      var response = await userAvatarUploadService.UploadUserAvatar(
        "johndoe@example.com",
        avatar
      );

      // Assert
      Assert.Equal(true, Guid.TryParse(response, out Guid guid));
    }

    [Fact]
    public async void Should_Be_Able_To_Update_User_Avatar() {
      // Arrange
      var user = new User() {
        Email = "johndoe@example.com",
        UserName = "johndoe@example.com",
        EmailConfirmed = true,
        PasswordHash = "password-123",
        Avatar = "default.png"
      };
      await usersRepository.Create(user, "some-role");

      FormFile oldFile;
      using (var stream = File.OpenRead("avatar-placeholder.png")) {
        oldFile = new FormFile(
          stream,
          0,
          stream.Length,
          null,
          Path.GetFileName(stream.Name)
        ) {
          Headers = new HeaderDictionary(),
          ContentType = "image/png"
        };
      };

      FormFile newFile;
      using (var stream = File.OpenRead("avatar-placeholder.png")) {
        newFile = new FormFile(
          stream,
          0,
          stream.Length,
          null,
          Path.GetFileName(stream.Name)
        ) {
          Headers = new HeaderDictionary(),
          ContentType = "image/png"
        };
      };

      var oldAvatar = new UserAvatarDTO(oldFile);
      var newAvatar = new UserAvatarDTO(newFile);

      await userAvatarUploadService.UploadUserAvatar(
        "johndoe@example.com",
        oldAvatar
      );

      // Act
      var response = await userAvatarUploadService.UploadUserAvatar(
        "johndoe@example.com",
        newAvatar
      );

      // Assert
      Assert.Equal(true, Guid.TryParse(response, out Guid guid));
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Upload_Avatar_If_User_Not_Exists() {
      // Arrange
      FormFile file;
      using (var stream = File.OpenRead("avatar-placeholder.png")) {
        file = new FormFile(
          stream,
          0,
          stream.Length,
          null,
          Path.GetFileName(stream.Name)
        ) {
          Headers = new HeaderDictionary(),
          ContentType = "image/png"
        };
      };
      var avatar = new UserAvatarDTO(file);

      // Act

      // Assert
      await Assert.ThrowsAsync<UserException>(
        () => userAvatarUploadService.UploadUserAvatar(
          "invalid.user@example.com",
          avatar
        )
      );
    }
  }
}