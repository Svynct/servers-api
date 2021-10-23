using ServerAPI.Domain.Entities;
using ServerAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Tests.FakeRepositories
{
    public class VideoRepositoryFake : IRepository<VideoEntity>
    {
        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<VideoEntity> InsertAsync(VideoEntity item)
        {
            throw new NotImplementedException();
        }

        public Task<VideoEntity> SelectAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoEntity>> SelectAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VideoEntity> UpdateAsync(VideoEntity item)
        {
            throw new NotImplementedException();
        }
    }
}
