using System;
using System.IO;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Tests.UnitTests.Mocks;
using ErrorCenter.Services.Providers.StorageProvider.Model;

namespace ErrorCenter.Tests.UnitTests.Services
{
    public class UserAvatarUploadServiceTest
    {
        private readonly Mock<IUsersRepository> usersRepository;
        private readonly Mock<IStorageProvider> storageProvider;
        private readonly IUserAvatarUploadService userAvatarUploadService;

        public UserAvatarUploadServiceTest()
        {
            usersRepository = new Mock<IUsersRepository>();
            storageProvider = new Mock<IStorageProvider>();
            userAvatarUploadService = new UserAvatarUploadService(
              usersRepository.Object,
              storageProvider.Object
            );
        }

        [Fact]
        public async void Should_Be_Able_To_Upload_Users_Avatar()
        {
            // Arrange
            var user = UserMock.UserFaker();

            FormFile file;
            using (var stream = File.OpenRead("avatar-placeholder.png"))
            {
                file = new FormFile(
                  stream,
                  0,
                  stream.Length,
                  null,
                  Path.GetFileName(stream.Name)
                )
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };
            };
            var avatar = new UserAvatarDTO() { avatar = file };

            usersRepository.Setup(x => x.FindByEmailTracking(user.Email)).ReturnsAsync(user);
            storageProvider.Setup(x => x.SaveFile(avatar.avatar)).Returns("saved-avatar.png");

            // Act
            var response = await userAvatarUploadService.UploadUserAvatar(
              user.Email,
              avatar
            );

            // Assert
            Assert.Equal("saved-avatar.png", response);
        }

        [Fact]
        public async void Should_Be_Able_To_Update_User_Avatar()
        {
            // Arrange
            var user = UserMock.UserFaker();
            user.Avatar = "old-avatar.png";

            FormFile file;
            using (var stream = File.OpenRead("avatar-placeholder.png"))
            {
                file = new FormFile(
                  stream,
                  0,
                  stream.Length,
                  null,
                  Path.GetFileName(stream.Name)
                )
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };
            };
            var avatar = new UserAvatarDTO() { avatar = file };

            usersRepository.Setup(x => x.FindByEmailTracking(user.Email)).ReturnsAsync(user);
            storageProvider.Setup(x => x.DeleteFile(user.Avatar));
            storageProvider.Setup(x => x.SaveFile(avatar.avatar)).Returns("new-avatar.png");

            // Act
            var response = await userAvatarUploadService.UploadUserAvatar(
              user.Email,
              avatar
            );

            // Assert
            Assert.Equal("new-avatar.png", response);
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Upload_Avatar_If_User_Not_Exists()
        {
            // Arrange
            var email = "invalid.user@example.com";

            FormFile file;
            using (var stream = File.OpenRead("avatar-placeholder.png"))
            {
                file = new FormFile(
                  stream,
                  0,
                  stream.Length,
                  null,
                  Path.GetFileName(stream.Name)
                )
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                };
            };
            var avatar = new UserAvatarDTO() { avatar = file };

            usersRepository.Setup(x => x.FindByEmailTracking(email)).ReturnsAsync((User)null);

            // Act

            // Assert
            await Assert.ThrowsAsync<UserException>(
              () => userAvatarUploadService.UploadUserAvatar(
                email,
                avatar
              )
            );
        }
    }
}