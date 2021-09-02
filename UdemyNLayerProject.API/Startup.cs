using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.UnitOfWorks;
using UdemyNLayerProject.Data;
using UdemyNLayerProject.Data.UnitOfWorks;

namespace UdemyNLayerProject.API
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
            //veri tabani sql server kullanacak,
            //veri tabani baglantisi icinde appsettings e ekledigim bu connection string i kullanacak.
            //Migration yapacagi icinde Seedlerin bulundugu Data assembly sini vermek gerek.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    Configuration["ConnectionStrings:SqlConStr"].ToString(),
                    o => {
                        o.MigrationsAssembly("UdemyNLayerProject.Data");
                    }
                    );
            }
            );

            //AddScoped() ne yapar?
            //Bir Request esnasinda IUnitOfWork ile bir classin constructer inda karsilasirsa;
            //gidecek UnitOfWork ten bir nesne ornegi alacak.
            //Bir request išerisinde birden fazla IUnitOfWork ile karsilasirsa
            //ayni UnitOfWork nesnesi ile devam edecek.

            //AddTransient() ne yapar?
            //Bir request išerisinde birden fazla IUnitOfWork ile karsilasirsa
            //her seferinde yeniden UnitOfWork nesnesi olusturur.
            //Buda performans acisindan tercih edilmez.

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
