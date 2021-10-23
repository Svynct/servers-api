using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServerAPI.Data.Context;
using ServerAPI.Data.Repository;
using ServerAPI.Domain;
using ServerAPI.Domain.Entities;
using ServerAPI.Domain.Interfaces;
using ServerAPI.Domain.Interfaces.Services;
using ServerAPI.Services.Services;

namespace ServerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            Globals.CONNECTION_STRING = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MyContext>(
                options => options.UseMySql(Globals.CONNECTION_STRING)
            );

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IRepository<ServerEntity>, BaseRepository<ServerEntity>>();
            services.AddScoped<IRepository<VideoEntity>, BaseRepository<VideoEntity>>();

            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IServerService, ServerService>();

            services.AddHostedService<RecyclerServiceWorker>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
               .AllowAnyHeader()
               .AllowAnyOrigin()
               .AllowAnyMethod());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
