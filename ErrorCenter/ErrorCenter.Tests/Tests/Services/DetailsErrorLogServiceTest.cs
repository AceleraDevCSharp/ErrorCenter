using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.IServices;
using ErrorCenter.Services.Services;
using ErrorCenter.Tests.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using Xunit;

namespace ErrorCenter.Tests.Tests.Services
{
    public class DetailsErrorLogServiceTest
    {
        private Mock<IErrorLogRepository<ErrorLog>> _errorLogRepository;
        private IDetailsErrorLogService service;

        public DetailsErrorLogServiceTest()
        {
            _errorLogRepository = new Mock<IErrorLogRepository<ErrorLog>>();
            service = new DetailsErrorLogService(_errorLogRepository.Object);
        }

        [Fact]
        public async void Should_be_Able_To_find_ErrorLog()
        {
            // Arrange

            var id = 10;

            var errorLog = ErrorLogMock.SingleErrorLogModelFaker();

            _errorLogRepository.Setup(x => x.FindById(id)).ReturnsAsync(
                errorLog
            );

            // Act

            var error = await service.FindErrorLog(id);

            // Assert

           Assert.NotNull(error);
        }
    }
}
