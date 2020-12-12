using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OkMusic.Models;
using Amazon.S3;
using OkMusic.Domain;
using OkMusic.Repositories;
using Evorine.FileSystem.S3FileProvider;
using Amazon;

namespace OkMusic
{
#pragma warning disable CS1591
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OkMusic", Version = "v1" });
            });

            // Sqlite数据库，https://docs.microsoft.com/zh-cn/ef/core/providers/sqlite/?tabs=dotnet-core-cli

            services.AddDbContext<OkMusicContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("OkMusicDatabase")));

            // https://docs.ceph.com/en/latest/radosgw/s3/csharp/
            services.AddTransient<Amazon.S3.IAmazonS3>(serviceProvider =>
                new AmazonS3Client(Configuration["S3:AccessKey"], Configuration["S3:SecretKey"], 
                new AmazonS3Config { ServiceURL = Configuration["S3:Endpoint"], ForcePathStyle = true }));

            services.AddTransient<UserRepository>();
            services.AddTransient<MusicFileRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OkMusic v1"));
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // https://github.com/evorine/S3FileProvider
            var amazonS3 = app.ApplicationServices.GetService<Amazon.S3.IAmazonS3>();
            var fileProvider = new S3FileProvider(amazonS3, Configuration["S3:BucketName"]);
            var staticFilesOption = new StaticFileOptions()
            {
                RequestPath = "/static",
                FileProvider = fileProvider
            };
            app.UseStaticFiles(staticFilesOption);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
#pragma warning disable CS1591
}
