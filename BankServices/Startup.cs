using BankServices.Models;
using BankServices.Services.Infrastructure;
using BankServices.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace BankServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            
            //services.AddDbContext<ClientContext>(options=>options.UseSqlServer(Configuration["ConnectionString:DefaultConnection"]), ServiceLifetime.Singleton);
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IAccountOperationRepository, AccountOperationRepository>();
            services.AddMvc();
        }
        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
