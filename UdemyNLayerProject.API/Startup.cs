using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyNLayerProject.API.Extensions;
using UdemyNLayerProject.API.Filters;
using UdemyNLayerProject.Core.Repositories;
using UdemyNLayerProject.Core.Services;
using UdemyNLayerProject.Core.UnitOfWorks;
using UdemyNLayerProject.Data;
using UdemyNLayerProject.Data.Repositories;
using UdemyNLayerProject.Data.UnitOfWorks;
using UdemyNLayerProject.Service.Services;

[assembly: ApiController]
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
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<NotFoundFilter>();
            //AddScoped() ne yapar?
            //Bir Request esnasinda IUnitOfWork ile bir classin constructer inda karsilasirsa;
            //gidecek UnitOfWork ten bir nesne ornegi alacak.
            //Bir request içerisinde birden fazla IUnitOfWork ile karsilasirsa
            //ayni UnitOfWork nesnesi ile devam edecek.

            //AddTransient() ne yapar?
            //Bir request içerisinde birden fazla IUnitOfWork ile karsilasirsa
            //her seferinde yeniden UnitOfWork nesnesi olusturur.
            //Buda performans acisindan tercih edilmez.

            //Genericler bu sekilde tanimlanir.
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));

            //Generic olmayanlar bu sekilde tanimlanir.
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
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

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //default false olur, kendim hatayi ele alacagimdan dolayi true yaptim
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomException();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
