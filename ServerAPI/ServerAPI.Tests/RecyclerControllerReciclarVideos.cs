using Xunit;
using ServerAPI.Services.Services;
using ServerAPI.Application.Controllers;
using ServerAPI.Tests.FakeRepositories;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Tests
{
    public class RecyclerControllerReciclarVideos
    {
        [Fact]
        public void QuandoExecutadoEndpointReciclarVideosComATarefaJaExecutandoDeveRetornar500EMensagem()
        {
            //Arrange
            RecyclerService.isExecutando = true;

            var videoRepo = new VideoRepositoryFake();
            var videoService = new VideoService(videoRepo);
            var controller = new RecyclerController(videoService);

            //Act
            var resultadoObtido = controller.ReciclarVideos(5).GetAwaiter().GetResult();

            //Assert
            Assert.IsType<ObjectResult>(resultadoObtido);
            var conteudoObtido = (resultadoObtido as ObjectResult).Value;
            Assert.Equal("Serviço de reciclagem já está em execução.", conteudoObtido);
        }
    }
}
