using Microsoft.AspNetCore.Mvc;
using ServerAPI.Domain.Interfaces.Services;
using ServerAPI.Domain.ViewModels;
using ServerAPI.Services.Services;
using System;
using System.Threading.Tasks;

namespace ServerAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecyclerController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public RecyclerController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        //■ /api/recycler/status
        [HttpGet, Route("status")]
        public ActionResult StatusReciclagem()
        {
            try
            {
                var isExecutando = RecyclerService.isExecutando;

                if (isExecutando == true)
                    return Ok(new StatusReciclagem { status = "running" });
                else
                    return Ok(new StatusReciclagem { status = "not running" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //○ Reciclar vídeos antigos
        //■ /api/recycler/process/{days}
        [HttpGet, Route("process/{days}")]
        public async Task<ActionResult> ReciclarVideos(int days)
        {
            try
            {
                if (RecyclerService.isExecutando == true)
                    throw new Exception("Serviço de reciclagem já está em execução.");

                var resposta = await _videoService.RemoveOldVideos(days);

                return StatusCode(202, $"Executando a recliclagem de vídeos com mais de {days} dias de existência na base.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
