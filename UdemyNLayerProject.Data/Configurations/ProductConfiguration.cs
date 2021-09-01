using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Data.Configurations
{
    //Product nesnesi veritabanında oluşturulurken hangi parametrelere göre oluşturulacak burada yazılır.
    //Configuration dosyası olması için IEntityTypeConfiguration ile tanımlamam lazım.
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            //3434.12 --- 5,2 olsaydı 123i45 şeklinde olacaktı.
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.Property(x => x.InnerBarkode).HasMaxLength(50);

            builder.ToTable("Products");
        }
    }
}
