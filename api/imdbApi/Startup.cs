using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using imdbApi.Repositories;
using ImdbDataRefresher;
using ImdbDataRefresher.Models;
using LiteDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace imdbApi
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
            services.AddImdbDataLoader ();
            services.AddTransient <LiteDatabase>((provider) => new LiteDatabase(Constants.LiteDbDatabaseName));
            RegisterRepositories (services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Movie>, MovieRepository> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            LoadApiDataIfMissing (app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void LoadApiDataIfMissing(IApplicationBuilder app) =>
            app.ApplicationServices.GetService <IDataRefresher> ().LoadImdbBasicsData ();
    }
}
