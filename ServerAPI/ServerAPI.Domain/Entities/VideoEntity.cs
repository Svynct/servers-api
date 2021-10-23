using System;

namespace ServerAPI.Domain.Entities
{
    public class VideoEntity : BaseEntity
    {
        public string Descricao { get; set; }
        public string Video { get; set; }
        public Guid ServerId { get; private set; }

        public VideoEntity() { }
        public VideoEntity(string descricao, string video)
        {
            Descricao = descricao;
            Video = video;
        }

        public void GravarVideo(Guid serverId)
        {
            ServerId = serverId;
        }
    }
}
