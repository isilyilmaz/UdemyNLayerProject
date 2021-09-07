using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Data.Configurations;
using UdemyNLayerProject.Data.Seeds;

namespace UdemyNLayerProject.Data
{
    /*DbContext miras alabilmesi için dependency den manage nuget manegement üzerinden;
     * Microsoft.EntityFrameworkCore eklemek gerek.
     * Microsoft.EntityFrameworkCore.SqlServer
     * Microsoft.EntityFrameworkCore.Tools
     * Microsoft.EntityFrameworkCore.Design     
     */
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Person> Persons { get; set; }

        //tablolar oluşurken oluşmadan önce çalışacak method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Best Practice açışından burada yapmak yerine ayrı bir class üzerinde builder ile parametreleri belirledim.

            //Önce Tablolar Oluşacak.
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());

            //Sonra Datalarımız İlgili Tablolara Eklenecek.
            modelBuilder.ApplyConfiguration(new ProductSeed(new int[] { 1, 2 }));
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] { 1, 2 }));

        }
    }
}
