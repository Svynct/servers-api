using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerAPI.Domain.Entities;

namespace ServerAPI.Data.Mapping
{
    public class VideoMap : IEntityTypeConfiguration<VideoEntity>
    {
        public void Configure(EntityTypeBuilder<VideoEntity> builder)
        {
            builder.ToTable("Videos");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Video)
                .IsRequired();

            builder.Property(v => v.Descricao)
                .HasMaxLength(100);

            builder.Property(v => v.ServerId);
        }
    }
}
