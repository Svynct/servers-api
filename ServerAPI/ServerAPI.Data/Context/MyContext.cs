using Microsoft.EntityFrameworkCore;
using ServerAPI.Data.Mapping;
using ServerAPI.Domain.Entities;

namespace ServerAPI.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<ServerEntity> Servers { get; set; }
        public DbSet<VideoEntity> Videos { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ServerEntity>(new ServerMap().Configure);
            modelBuilder.Entity<VideoEntity>(new VideoMap().Configure);
        }
    }
}
