using ServerAPI.Domain.Entities;
using ServerAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Domain.Interfaces.Services
{
    public interface IServerService
    {
        Task<ServerEntity> CreateServerAsync(ServerViewModel view);
        Task<IEnumerable<ServerEntity>> SelectAllServersAsync();
        Task<bool> IsServerEnabledAsync(Guid id);
        Task<bool> DeleteServerAsync(Guid id);
        Task<ServerEntity> SelectServerAsync(Guid id);
    }
}
