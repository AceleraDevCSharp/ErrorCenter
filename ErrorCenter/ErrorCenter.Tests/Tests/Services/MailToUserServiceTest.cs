using ErrorCenter.Services.IServices;
using ErrorCenter.Services.Services;
using Moq;
using Xunit;
using ErrorCenter.Tests.UnitTests.Mocks;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ErrorCenter.Tests.Tests.Services
{
    public class MailToUserServiceTest
    {
        private Mock<IUsersRepository> _userRepository;
        private IMailToUserService _mailToUserService;

        public MailToUserServiceTest()
        {
            _userRepository = new Mock<IUsersRepository>();
            _mailToUserService = new MailToUserService(_userRepository.Object);
        }

        [Fact]
        public async void Should_Be_Able_To_Send_Mail_To_User()
        {
            // Arrange

            var mail = "abcd@gmail.com";

            _userRepository.Setup(x => x.FindByEmail(mail)).ReturnsAsync(UserMock.UserFaker());

            // Act

            var response = await _mailToUserService.MailToUser(mail);

            // Assert

            Assert.NotNull(response);
            Assert.NotEmpty(response);
            Assert.Equal("Mail sended!", response);

        }

    }
}
