using Microsoft.Extensions.Hosting;
using ServerAPI.Data.Context;
using ServerAPI.Data.FileServer;
using ServerAPI.Data.Repository;
using ServerAPI.Domain.Entities;
using ServerAPI.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ServerAPI.Services.Services
{
    public class RecyclerServiceWorker : IHostedService
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private readonly IRepository<VideoEntity> _videoRepository = new BaseRepository<VideoEntity>(new ContextFactory().CreateDbContext(new string[]{}));

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }
            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                await Process();
                await Task.Delay(5000, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);
        }
        protected async Task Process()
        {
            do
            {
                //Checa SE é pra rodar o serviço de 10 em 10 segundos.
                await Task.Delay(10000);
            }
            while (!RecyclerService.isExecutando);

            try
            {
                var videos = await _videoRepository.SelectAsync();

                var qtdDias = DateTime.UtcNow.AddDays(RecyclerService.Dias * -1);

                videos = videos.Where(v => v.CreatedAt < qtdDias).ToList();

                foreach (var video in videos)
                {
                    var removerDoFileServer = Helper.RemoverVideo(video.ServerId, video.Video);
                    await _videoRepository.DeleteAsync(video.Id);
                }

                RecyclerService.Dias = 0;
                RecyclerService.isExecutando = false;
            }
            catch (Exception)
            {
                RecyclerService.Dias = 0;
                RecyclerService.isExecutando = false;
            }
        }
    }
}
