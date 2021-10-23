using ServerAPI.Domain.Entities;
using ServerAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Domain.Interfaces.Services
{
    public interface IVideoService
    {
        Task<VideoEntity> AddVideoToServerAsync(Guid serverId, VideoViewModel view);
        Task<bool> DeleteVideoFromServerAsync(Guid serverId, Guid videoId);
        Task<VideoEntity> SelectVideoFromServerAsync(Guid serverId, Guid videoId);
        Task<byte[]> DownloadBinariesFromVideoAsync(Guid serverId, Guid videoId);
        Task<IEnumerable<VideoEntity>> SelectAllVideosFromServerAsync(Guid serverId);
        Task<bool> RemoveOldVideos(int dias);
    }
}
