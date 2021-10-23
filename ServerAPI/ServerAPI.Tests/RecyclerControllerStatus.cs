using Microsoft.AspNetCore.Mvc;
using ServerAPI.Application.Controllers;
using ServerAPI.Domain.ViewModels;
using ServerAPI.Services.Services;
using ServerAPI.Tests.FakeRepositories;
using Xunit;

namespace ServerAPI.Tests
{
    public class RecyclerControllerStatus
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void QuandoExecutadoEndpointStatusDeveRetornarEstadoDaTarefa(bool isExecutando)
        {
            //Arrange
            RecyclerService.isExecutando = isExecutando;

            var videoRepo = new VideoRepositoryFake();
            var videoService = new VideoService(videoRepo);
            var controller = new RecyclerController(videoService);

            //Act
            var resultadoObtido = controller.StatusReciclagem();

            //Assert
            Assert.IsType<OkObjectResult>(resultadoObtido);
            var conteudoObtido = (resultadoObtido as OkObjectResult).Value;
            var status = (conteudoObtido as StatusReciclagem).status;
            if (isExecutando)
                Assert.Equal("running", status);
            else
                Assert.Equal("not running", status);
        }
    }
}
