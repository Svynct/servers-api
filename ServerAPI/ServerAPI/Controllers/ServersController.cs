using Microsoft.AspNetCore.Mvc;
using ServerAPI.Domain.Interfaces.Services;
using ServerAPI.Domain.ViewModels;
using System;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly IServerService _serverService;
        private readonly IVideoService _videoService;

        public ServersController(IServerService serverService, IVideoService videoService)
        {
            _serverService = serverService;
            _videoService = videoService;
        }

        //○ Listar todos os servidores
        //■ /api/servers
        [HttpGet, Route("")]
        public async Task<ActionResult> ListarServidores()
        {
            try
            {
                var servidores = await _serverService.SelectAllServersAsync();

                return Ok(servidores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Criar um novo servidor
        //■ /api/server
        [HttpPost, Route("")]
        public async Task<ActionResult> CriarServidor([FromBody] ServerViewModel view)
        {
            try
            {
                var servidor = await _serverService.CreateServerAsync(view);

                return Ok(servidor);
            }
            catch (Exception ex)
            {
                if (ex.Message == "O modelo enviado não possui todos os dados necessários para a criação do servidor.")
                    return BadRequest(ex.Message);
                else
                    return StatusCode(500, ex.Message);
            }
        }

        //○ Checar disponibilidade de um servidor
        //■ /api/servers/available/{serverId}
        [HttpGet, Route("available/{serverId}")]
        public async Task<ActionResult> ChecarDisponibilidade(string serverId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serverId))
                    return BadRequest("Você precisa enviar um Id válido na rota.");

                var id = Guid.Parse(serverId);

                var disponivel = await _serverService.IsServerEnabledAsync(id);

                if (disponivel)
                    return Ok($"O servidor {id} está disponível.");
                else
                    return Ok($"O servidor {id} está indisponível.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //○ Recuperar um servidor existente
        //■ /api/servers/{serverId}
        [HttpGet, Route("{serverId}")]
        public async Task<ActionResult> RecuperarServidor(string serverId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serverId))
                    return BadRequest("Você precisa enviar um Id válido na rota.");

                var id = Guid.Parse(serverId);

                var servidor = await _serverService.SelectServerAsync(id);

                return Ok(servidor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //○ Remover um servidor existente
        //■ /api/servers/{serverId}
        [HttpDelete, Route("{serverId}")]
        public async Task<ActionResult> DeletarServidor(string serverId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serverId))
                    return BadRequest("Você precisa enviar um Id válido na rota.");

                var id = Guid.Parse(serverId);

                var deletado = await _serverService.DeleteServerAsync(id);

                if (deletado)
                    return Ok($"O servidor {id} foi deletado com sucesso.");
                else
                    return Ok($"O servidor {id} não pôde ser deletado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //○ Adicionar um novo vídeo à um servidor
        //■ /api/servers/{serverId}/videos
        [HttpPost, Route("{serverId}/videos")]
        public async Task<ActionResult> AdicionarVideo([FromRoute] string serverId, [FromBody] VideoViewModel view)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serverId))
                    return BadRequest("Você precisa enviar um Id válido na rota.");

                var id = Guid.Parse(serverId);

                var servidor = await _videoService.AddVideoToServerAsync(id, view);

                return StatusCode(201, servidor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //○ Remover um vídeo existente
        //■ /api/servers/{serverId}/videos/{videoId} 
        [HttpDelete, Route("{serverId}/videos/{videoId}")]
        public async Task<ActionResult> RemoverVideo(string serverId, string videoId, [FromBody] string base64video)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serverId) || string.IsNullOrWhiteSpace(videoId))
                    return BadRequest("Você precisa enviar um Id válido na rota.");

                var idServer = Guid.Parse(serverId);
                var idVideo = Guid.Parse(videoId);

                var deletado = await _videoService.DeleteVideoFromServerAsync(idServer, idVideo);

                if (deletado)
                    return Ok($"O video {idVideo} foi deletado com sucesso do servidor {idServer}.");
                else
                    return Ok($"O video {idVideo} não pôde ser deletado do servidor {idServer}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //○ Listar todos os vídeos de um servidor
        //■ /api/servers/{serverId}/videos
        [HttpGet, Route("{serverId}/videos")]
        public async Task<ActionResult> RecuperarVideos(string serverId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serverId))
                    return BadRequest("Você precisa enviar um Id válido na rota.");

                var id = Guid.Parse(serverId);

                var videos = await _videoService.SelectAllVideosFromServerAsync(id);

                return Ok(videos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //○ Retorna o conteúdo binário de um vídeo
        //■ /api/servers/{serverId}/videos/{videoId}/binary
        [HttpGet, Route("{serverId}/videos/{videoId}/binary")]
        public async Task<ActionResult> DownloadVideo(string serverId, string videoId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serverId) || string.IsNullOrWhiteSpace(videoId))
                    return BadRequest("Você precisa enviar um Id válido na rota.");

                var idServer = Guid.Parse(serverId);
                var idVideo = Guid.Parse(videoId);

                var videos = await _videoService.DownloadBinariesFromVideoAsync(idServer, idVideo);

                return Ok(videos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //○ Recuperar dados cadastrais de um vídeo
        //■ /api/servers/{serverId}/videos/{videoId} 
        [HttpGet, Route("{serverId}/videos/{videoId}")]
        public async Task<ActionResult> RecuperarVideo(string serverId, string videoId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(serverId) || string.IsNullOrWhiteSpace(videoId))
                    return BadRequest("Você precisa enviar um Id válido na rota.");

                var idServer = Guid.Parse(serverId);
                var idVideo = Guid.Parse(videoId);

                var video = await _videoService.SelectVideoFromServerAsync(idServer, idVideo);

                return Ok(video);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
