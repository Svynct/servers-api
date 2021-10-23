using ServerAPI.Domain.Entities;
using ServerAPI.Domain.Interfaces;
using ServerAPI.Domain.Interfaces.Services;
using ServerAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Services.Services
{
    public class ServerService : IServerService
    {
        private readonly IRepository<ServerEntity> _serverRepository;

        public ServerService(IRepository<ServerEntity> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<ServerEntity> CreateServerAsync(ServerViewModel view)
        {
            try
            {
                if (view.Nome == null || view.IP == null)
                    throw new Exception("O modelo enviado não possui todos os dados necessários para a criação do servidor.");

                var server = new ServerEntity(view.Nome, view.IP, view.Porta);

                return await _serverRepository.InsertAsync(server);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteServerAsync(Guid id)
        {
            try
            {
                return await _serverRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> IsServerEnabledAsync(Guid id)
        {
            try
            {
                var server = await _serverRepository.SelectAsync(id);

                return server.IsDisponivel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ServerEntity>> SelectAllServersAsync()
        {
            try
            {
                return await _serverRepository.SelectAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ServerEntity> SelectServerAsync(Guid id)
        {
            try
            {
                return await _serverRepository.SelectAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
