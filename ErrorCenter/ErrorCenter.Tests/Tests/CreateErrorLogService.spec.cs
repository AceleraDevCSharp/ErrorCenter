using System;

using Xunit;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Services.Fakes;
using ErrorCenter.Services.DTOs;

namespace ErrorCenter.UnitTests
{
    public class CreateErrorLogServiceTest
    {
        private IUsersRepository usersRepository;
        private IErrorLogRepository<ErrorLog> errorLogsRepository;
        private IErrorLogService service;
        private IEnvironmentsRepository environmentsRepository;

        public CreateErrorLogServiceTest()
        {
            usersRepository = new FakeUsersRepository();
            errorLogsRepository = new FakeErrorLogsRepository();
            environmentsRepository = new FakeEnvironmentsRepository();
            service = new ErrorLogService(usersRepository, environmentsRepository, errorLogsRepository, null);
        }

        [Fact]
        public async void Should_Be_Able_To_Create_An_Error_Log()
        {
            // Arrange
            var environment = new Persistence.EF.Models.Environment()
            {
                Name = "Development",
                NormalizedName = "DEVELOPMENT"
            };

            await environmentsRepository.Create(environment);

            var errologDTO = new ErrorLogDTO()
            {
                Environment = "Development",
                Details = "Detalhes1",
                Level = "Level1",
                Origin = "Origem1",
                Title = "Titulo1"

            };

            var user = new User()
            {
                Email = "john1@example.com",
                UserName = "john1@example.com",
                EmailConfirmed = true,
            };
            var userEnvironment = errologDTO.Environment;

            await usersRepository.Create(user, errologDTO.Environment);

            var Created = await service.CreateNewErrorLog(errologDTO, user.Email, userEnvironment);

            // Assert
            Assert.Equal(Created, errologDTO);
            //Assert.Equals(Created, errologDTO);
        }


        [Fact]
        public async void Should_Not_Able_To_Create_Error_If_Not_Same_Environment()
        {
            // Arrange
            var environment = new Persistence.EF.Models.Environment()
            {
                Name = "Development",
                NormalizedName = "DEVELOPMENT"
            };

            await environmentsRepository.Create(environment);

            var errologDTO = new ErrorLogDTO()
            {
                Environment = "Development",
                Details = "Detalhes1",
                Level = "Level1",
                Origin = "Origem1",
                Title = "Titulo1"

            };

            var user = new User()
            {
                Email = "john2@example.com",
                UserName = "john2@example.com",
                EmailConfirmed = true,
            };
            var userResponse = await usersRepository.Create(user, "OtherEnvironment");
            var userEnvironment = "OtherEnvironment";

            // Assert
            await Assert.ThrowsAsync<UserException>(
              () => service.CreateNewErrorLog(errologDTO, user.Email, userEnvironment)
            );
        }

        //[Fact]
        //public async void Should_Not_Be_Able_To_Delete_Deleted_Error() {
        //  // Arrange
        //  var user = new User() {
        //    Email = "johndoe@example.com",
        //    UserName = "johndoe@example.com",
        //    EmailConfirmed = true,
        //  };

        //  var errorLog = new ErrorLog() {
        //    Id = 1,
        //    Environment = new Persistence.EF.Models.Environment() {
        //      Name = "Development",
        //      NormalizedName = "DEVELOPMENT"
        //    },
        //    CreatedAt = DateTime.Now,
        //    DeletedAt = DateTime.Now,
        //  };

        //  // Act
        //  await usersRepository.Create(user, "Development");
        //  await errorLogsRepository.Create(errorLog);

        //  // Assert
        //  await Assert.ThrowsAsync<ErrorLogException>(
        //    () => service.DeleteErrorLog(1, user.Email, "Development")
        //  );
        //}
    }
}