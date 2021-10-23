using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerAPI.Domain.Entities;

namespace ServerAPI.Data.Mapping
{
    public class ServerMap : IEntityTypeConfiguration<ServerEntity>
    {
        public void Configure(EntityTypeBuilder<ServerEntity> builder)
        {
            builder.ToTable("Servers");

            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.IP)
                .IsUnique();

            builder.Property(s => s.Porta)
                .IsRequired();

            builder.Property(s => s.Nome)
                .HasMaxLength(100);
        }
    }
}
