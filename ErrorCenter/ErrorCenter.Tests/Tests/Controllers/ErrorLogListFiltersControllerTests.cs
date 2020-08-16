using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.IServices;
using ErrorCenter.Tests.UnitTests.Mocks;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using ErrorCenter.WebAPI.Controllers;
using ErrorCenter.WebAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using m = ErrorCenter.Persistence.EF.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace ErrorCenter.Tests.UnitTests.Controllers
{

    public class ErrorLogListFiltersControllerTests
    {
        private readonly Mock<IErrorLogRepository<ErrorLog>> _errorLogRepositoryMock;
        private readonly Mock<IMapper> _mapper;

        public ErrorLogListFiltersControllerTests()
        {
            _errorLogRepositoryMock = new Mock<IErrorLogRepository<ErrorLog>>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Should_be_Able_Return_All_Error_Log_controller()
        {
            _errorLogRepositoryMock.Setup(x => x.SelectAll())
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetAll();

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Fact]
        public async Task Should_be_Able_Return_All_Error_Log_Arquived_controller()
        {
            _errorLogRepositoryMock.Setup(x => x.SelectArchived())
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetLogsArchived();

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Fact]
        public async Task Should_be_Able_Return_All_Error_Log_Deleted_controller()
        {
            _errorLogRepositoryMock.Setup(x => x.SelectDeleted())
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetLogsDeleted();

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Theory]
        [InlineData("Development")]
        [InlineData("Homologation")]
        [InlineData("Production")]
        public async Task Should_be_Able_Return_All_Error_Log_By_Enviroment_controller(string env)
        {
            _errorLogRepositoryMock.Setup(x => x.SelectByEnvironment(env))
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetByEnvironment(env);

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Theory]
        [InlineData("Development", "Level")]
        [InlineData("Development", "Frequencia")]
        [InlineData("Homologation", "Level")]
        [InlineData("Homologation", "Frequencia")]
        [InlineData("Production", "Level")]
        [InlineData("Production", "Frequencia")]
        public async Task Should_be_Able_Return_All_Error_Log_By_Enviroment_And_Ordered_By_controller(string env, string order)
        {
            _errorLogRepositoryMock.Setup(x => x.SelectByEnvironmentOrderedBy(env, order))
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetByEnvironmentOrderBy(env, order);

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Theory]
        [InlineData("Development", "Level", "Level", "a")]
        [InlineData("Development", "Frequencia", "Level", "a")]
        [InlineData("Development", "Level", "Descricao", "a")]
        [InlineData("Development", "Frequencia", "Descricao", "a")]
        [InlineData("Development", "Level", "Origem", "a")]
        [InlineData("Development", "Frequencia", "Origem", "a")]
        [InlineData("Homologation", "Level", "Level", "a")]
        [InlineData("Homologation", "Frequencia", "Level", "a")]
        [InlineData("Homologation", "Level", "Descricao", "a")]
        [InlineData("Homologation", "Frequencia", "Descricao", "a")]
        [InlineData("Homologation", "Level", "Origem", "a")]
        [InlineData("Homologation", "Frequencia", "Origem", "a")]
        [InlineData("Production", "Level", "Level", "a")]
        [InlineData("Production", "Frequencia", "Level", "a")]
        [InlineData("Production", "Level", "Descricao", "a")]
        [InlineData("Production", "Frequencia", "Descricao", "a")]
        [InlineData("Production", "Level", "Origem", "a")]
        [InlineData("Production", "Frequencia", "Origem", "a")]
        public async Task Should_be_Able_Return_All_Error_Log_By_Enviroment_And_Ordered_By_Search_By_controller(string env, string order, string search, string textSearch)
        {
            _errorLogRepositoryMock.Setup(x => x.SelectByEnvironmentOrderedBySearchBy(env, order, search, textSearch))
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetByEnvironmentOrderBySearchBy(env, order, search, textSearch);

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Theory]
        [InlineData("Development", "Level", "a")]
        [InlineData("Development", "Descricao", "a")]
        [InlineData("Development", "Origem", "a")]
        [InlineData("Homologation", "Level", "a")]
        [InlineData("Homologation", "Descricao", "a")]
        [InlineData("Homologation", "Origem", "a")]
        [InlineData("Production", "Level", "a")]
        [InlineData("Production", "Descricao", "a")]
        [InlineData("Production", "Origem", "a")]
        public async Task Should_be_Able_Return_All_Error_Log_By_Enviroment_Search_By_controller(string env, string search, string textSearch)
        {
            _errorLogRepositoryMock.Setup(x => x.SelectByEnvironmentSearchBy(env, search, textSearch))
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetByEnvironmentSearchBy(env, search, textSearch);

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Theory]
        [InlineData("Level")]
        [InlineData("Frequencia")]
        public async Task Should_be_Able_Return_All_Error_Log_Ordered_By_controller(string order)
        {
            _errorLogRepositoryMock.Setup(x => x.SelectOrderedBy(order))
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetOrderedBy(order);

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Theory]
        [InlineData("Level", "Level", "a")]
        [InlineData("Level", "Descricao", "a")]
        [InlineData("Level", "Origem", "a")]
        [InlineData("Frequencia", "Level", "a")]
        [InlineData("Frequencia", "Descricao", "a")]
        [InlineData("Frequencia", "Origem", "a")]
        public async Task Should_be_Able_Return_All_Error_Log_Ordered_By_Search_By_controller(string order, string search, string textSearch)
        {
            _errorLogRepositoryMock.Setup(x => x.SelectOrderedBySearchBy(order, search, textSearch))
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetOrderedBySearchBy(order, search, textSearch);

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Theory]
        [InlineData("Level", "a")]
        [InlineData("Descricao", "a")]
        [InlineData("Origem", "a")]
        public async Task Should_be_Able_Return_All_Error_Log_Search_By_controller(string search, string textSearch)
        {
            _errorLogRepositoryMock.Setup(x => x.SelectSearchBy(search, textSearch))
                .ReturnsAsync(ErrorLogMock.ErrorLogModelFaker);

            _mapper.Setup(x => x.Map<IEnumerable<ErrorLogViewModel>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.ErrorLogViewModelFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetSearchBy(search, textSearch);

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<ErrorLogViewModel>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }

        [Fact]
        public async Task Should_be_Able_Return_All_Enviroments_controller()
        {
            _errorLogRepositoryMock.Setup(x => x.Environments())
                .ReturnsAsync(ErrorLogMock.EnviromentsFaker);

            _mapper.Setup(x => x.Map<IEnumerable<m.Environment>>(It.IsAny<IEnumerable<ErrorLog>>()))
               .Returns(ErrorLogMock.EnviromentsFaker);

            var controller = new ErrorLogListFiltersController(_errorLogRepositoryMock.Object, _mapper.Object);

            var route = await controller.GetEnvironments();

            var actionResult = Assert.IsType<OkObjectResult>(route.Result);
            var actionValue = Assert.IsAssignableFrom<IEnumerable<m.Environment>>(actionResult.Value);

            Assert.NotNull(actionResult);
            Assert.Equal(StatusCodes.Status200OK, actionResult.StatusCode);
        }
    }
}
