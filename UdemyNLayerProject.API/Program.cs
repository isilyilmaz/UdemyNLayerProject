using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyNLayerProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*Dependency injection kaba tabir ile bir sınıfın/nesnenin bağımlılıklardan kurtulmasını amaçlayan 
             * ve o nesneyi olabildiğince bağımsızlaştıran bir programlama tekniği/prensibidir.
            */

            //KULLANILAN PATTERNLER:
            /*1-Generic Repository Pattern ne sağlar?
             * Veriye tek bir noktadan erişmeyi sağlar.
             * Kod tekrarını önler.
             * Veri tabanına ulaşım merkezi bir yerden olur.       
             * Farklı db kullanımında, dependency injection sayesinde kod yapısını bozmadan çok rahat implemente edilebilir.
             */

            //2-UnityOfWork Pattern, ilgili nesnelerin verilerin veri tabanına tek biryerden yansımasını sağlayan patterndir.


            //KATMANLAR:
            //1-Core katmanı içinde interface ve model(entity) dışında birşey bulunmuyor.

            /* 2-Data katmanı veri tabanı ile haberleşecek olan katman. İçinde; 
             * Migration dosyaları, 
             * seed(veri tabanında tablolar oluştuğunda default kayıtlar oluşturulur,bunlar seed üzerinden yönetilir) dosyaları,
             * Repository(Generic Repository Pattern kullanılarak implemente edildi),
             * DBContext (EntityFramework kullanılacak.)
             * olacek.
             */

            //3-Service katmanı içinde Service ve business code lar olacak.

            //4-API, Service katmanı ile haberleşecek.

            /*5-Logging için, proje daha büyürse loglama makenizması gerekecek.
             * Bunun için Logging katmanı oluşturmak gerekecek.*/

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
