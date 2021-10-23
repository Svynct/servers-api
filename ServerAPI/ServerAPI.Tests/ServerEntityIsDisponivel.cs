using Xunit;
using ServerAPI.Domain.Entities;
using System.Net.NetworkInformation;

namespace ServerAPI.Tests
{
    public class ServerEntityIsDisponivel
    {
        [Theory]
        [InlineData("Google", "www.google.com", 0)]
        [InlineData("Outlook", "www.outlook.com", 0)]
        [InlineData("Facebook", "www.facebook.com", 0)]
        [InlineData("Instagram", "www.instagram.com", 0)]
        public void QuandoIsDisponivelEhDisparadoParaUmHostValidoDeveRetornarTrue(string nome, string ip, int porta)
        {
            //Arrange
            var server = new ServerEntity(nome, ip, porta);

            //Act
            var isDisponivel = server.IsDisponivel();

            //Assert
            Assert.True(isDisponivel);
        }

        [Fact]
        public void QuandoIsDisponivelEhDisparadoParaUmHostInvalidoDeveRetornarPingException()
        {
            //Arrange
            var server = new ServerEntity("Host Invalido", "www.hostinvalido.com.br", 6060);

            //Assert
            Assert.Throws<PingException>(
                //Act
                () => server.IsDisponivel()
            );
        }
    }
}
