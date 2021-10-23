using ServerAPI.Data.FileServer;
using ServerAPI.Domain.Entities;
using ServerAPI.Domain.Interfaces;
using ServerAPI.Domain.Interfaces.Services;
using ServerAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerAPI.Services.Services
{
    public class VideoService : IVideoService
    {
        private readonly IRepository<VideoEntity> _videoRepository;

        public VideoService(IRepository<VideoEntity> videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public async Task<VideoEntity> AddVideoToServerAsync(Guid serverId, VideoViewModel view)
        {
            try
            {
                var fileName = $"{serverId}-{ConvertToTimestamp(DateTime.Now)}";

                var sucesso = Helper.SalvarVideo(serverId, fileName, view.Video);

                if (!sucesso)
                    throw new Exception("O vídeo não pôde ser salvo no servidor.");

                var video = new VideoEntity(view.Descricao, fileName);

                video.GravarVideo(serverId);

                var videoInserido = await _videoRepository.InsertAsync(video);

                return videoInserido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteVideoFromServerAsync(Guid serverId, Guid videoId)
        {
            try
            {
                var video = await _videoRepository.SelectAsync(videoId);

                if (video == null)
                    throw new Exception($"O vídeo {videoId} não foi encontrado.");

                var removerDoFileServer = Helper.RemoverVideo(serverId, video.Video);

                var removidoVideo = await _videoRepository.DeleteAsync(videoId);

                if (!removidoVideo)
                    throw new Exception($"O vídeo {videoId} não foi encontrado.");

                return removidoVideo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<byte[]> DownloadBinariesFromVideoAsync(Guid serverId, Guid videoId)
        {
            try
            {
                var video = await _videoRepository.SelectAsync(videoId);

                if (video == null)
                    throw new Exception($"O vídeo {videoId} não foi encontrado.");

                var retorno = Helper.RecuperarConteudoDoVideo(serverId, video.Video);

                retorno = retorno.Replace("data:video/mp4;base64,", "");

                return Convert.FromBase64String(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<VideoEntity>> SelectAllVideosFromServerAsync(Guid serverId)
        {
            try
            {
                var videos = await _videoRepository.SelectAsync();

                videos = videos.Where(v => v.ServerId == serverId).ToList();

                return videos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<VideoEntity> SelectVideoFromServerAsync(Guid serverId, Guid videoId)
        {
            try
            {
                var video = await _videoRepository.SelectAsync(videoId);

                if (video.ServerId != serverId)
                    throw new Exception($"O vídeo procurado não pertence ao servidor {serverId}.");
                
                return video;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveOldVideos(int dias)
        {
            try
            {
                if (RecyclerService.isExecutando)
                    throw new Exception("Serviço de reciclagem já está em execução.");

                RecyclerService.isExecutando = true;
                RecyclerService.Dias = dias;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private long ConvertToTimestamp(DateTime value)
        {
            var Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds;
        }
    }
}
